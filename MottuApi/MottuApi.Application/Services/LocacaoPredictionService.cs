using Microsoft.ML;
using Microsoft.ML.Data;
using MottuApi.Application.Interfaces;

namespace MottuApi.Application.Services
{
    public class LocacaoPredictionService : ILocacaoPredictionService
    {
        private readonly MLContext _mlContext = new MLContext(seed: 1);
        private ITransformer? _model;

        public Task<decimal> PreverValorTotalAsync(int horas, int anoMoto, decimal valorHora)
        {
            try
            {
                _model ??= TrainModel();
                var engine = _mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(_model);
                var prediction = engine.Predict(new ModelInput
                {
                    Horas = horas,
                    AnoMoto = anoMoto,
                    ValorHora = (float)valorHora,
                    ValorTotal = 0
                });
                var result = (decimal)Math.Max(0, prediction.Score);
                return Task.FromResult(result);
            }
            catch
            {
                // Fallback determinístico caso ML.NET não esteja disponível no ambiente
                return Task.FromResult(horas * valorHora);
            }
        }

        private ITransformer TrainModel()
        {
            var trainingData = CreateSyntheticData();
            var dataView = _mlContext.Data.LoadFromEnumerable(trainingData);
            var pipeline = _mlContext.Transforms.Concatenate("Features", nameof(ModelInput.Horas), nameof(ModelInput.AnoMoto), nameof(ModelInput.ValorHora))
                .Append(_mlContext.Regression.Trainers.Sdca(labelColumnName: nameof(ModelInput.ValorTotal)));
            return pipeline.Fit(dataView);
        }

        private static IEnumerable<ModelInput> CreateSyntheticData()
        {
            // Gera dados simples: valor total ~ horas * valorHora com leve penalização para motos mais antigas
            var baseYear = DateTime.UtcNow.Year;
            var list = new List<ModelInput>();
            foreach (var ano in new[] { baseYear - 10, baseYear - 5, baseYear, baseYear + 1 })
            {
                foreach (var horas in new[] { 1, 2, 4, 8, 12, 24 })
                {
                    foreach (var valorHora in new[] { 10m, 15m, 20m, 30m })
                    {
                        var fatorAno = 1m - Math.Clamp((baseYear - ano) * 0.01m, 0m, 0.2m);
                        var valor = horas * valorHora * fatorAno;
                        list.Add(new ModelInput
                        {
                            Horas = horas,
                            AnoMoto = ano,
                            ValorHora = (float)valorHora,
                            ValorTotal = (float)valor
                        });
                    }
                }
            }
            return list;
        }

        private class ModelInput
        {
            public int Horas { get; set; }
            public int AnoMoto { get; set; }
            public float ValorHora { get; set; }
            public float ValorTotal { get; set; }
        }

        private class ModelOutput
        {
            [ColumnName("Score")]
            public float Score { get; set; }
        }
    }
}



