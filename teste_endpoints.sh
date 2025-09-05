#!/bin/bash

# =====================================================
# SCRIPT DE TESTE DOS ENDPOINTS - MOTTU API
# =====================================================

echo "🚀 INICIANDO TESTES DOS ENDPOINTS - MOTTU API"
echo "=============================================="

# Cores para output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
NC='\033[0m' # No Color

# Função para testar endpoint
test_endpoint() {
    local method=$1
    local url=$2
    local data=$3
    local description=$4
    
    echo -e "\n${BLUE}🧪 Testando: $description${NC}"
    echo "   $method $url"
    
    if [ -n "$data" ]; then
        response=$(curl -s -w "\n%{http_code}" -X $method "$url" \
            -H "Content-Type: application/json" \
            -d "$data" 2>/dev/null)
    else
        response=$(curl -s -w "\n%{http_code}" -X $method "$url" \
            -H "accept: application/json" 2>/dev/null)
    fi
    
    http_code=$(echo "$response" | tail -n1)
    body=$(echo "$response" | head -n -1)
    
    if [ "$http_code" -ge 200 ] && [ "$http_code" -lt 300 ]; then
        echo -e "   ${GREEN}✅ Status: $http_code${NC}"
        if [ -n "$body" ] && [ "$body" != "null" ]; then
            echo "   📄 Response: $body" | head -c 100
            echo "..."
        fi
    else
        echo -e "   ${RED}❌ Status: $http_code${NC}"
        echo "   📄 Response: $body"
    fi
}

# Verificar se a API está rodando
echo -e "\n${YELLOW}🔍 Verificando se a API está rodando...${NC}"
if curl -s -o /dev/null -w "%{http_code}" http://localhost:5000 | grep -q "200\|301\|302"; then
    echo -e "${GREEN}✅ API está rodando na porta 5000${NC}"
else
    echo -e "${RED}❌ API não está rodando. Inicie com: dotnet run${NC}"
    exit 1
fi

# Testes dos endpoints
echo -e "\n${YELLOW}📋 TESTANDO ENDPOINTS DE FILIAIS${NC}"
echo "=================================="

test_endpoint "GET" "http://localhost:5000/api/filial" "" "Listar todas as filiais"
test_endpoint "GET" "http://localhost:5000/api/filial/1" "" "Buscar filial por ID"
test_endpoint "GET" "http://localhost:5000/api/filial/999" "" "Buscar filial inexistente"

echo -e "\n${YELLOW}📋 TESTANDO ENDPOINTS DE MOTOS${NC}"
echo "=================================="

test_endpoint "GET" "http://localhost:5000/api/moto" "" "Listar todas as motos"
test_endpoint "GET" "http://localhost:5000/api/moto/1" "" "Buscar moto por ID"
test_endpoint "GET" "http://localhost:5000/api/moto/por-placa?placa=ABC1234" "" "Buscar moto por placa"
test_endpoint "GET" "http://localhost:5000/api/moto/por-filial/1" "" "Listar motos de uma filial"

echo -e "\n${YELLOW}📋 TESTANDO CRIAÇÃO DE DADOS${NC}"
echo "=================================="

# Dados para teste
filial_data='{
    "nome": "Filial Teste",
    "logradouro": "Rua Teste",
    "numero": "123",
    "complemento": "Sala 1",
    "bairro": "Centro",
    "cidade": "São Paulo",
    "estado": "SP",
    "cep": "01234567",
    "telefone": "(11) 99999-9999"
}'

moto_data='{
    "placa": "TEST123",
    "modelo": "Honda Test",
    "ano": 2024,
    "cor": "Azul",
    "filialId": 1
}'

test_endpoint "POST" "http://localhost:5000/api/filial" "$filial_data" "Criar nova filial"
test_endpoint "POST" "http://localhost:5000/api/moto" "$moto_data" "Criar nova moto"

echo -e "\n${YELLOW}📋 TESTANDO ATUALIZAÇÃO DE DADOS${NC}"
echo "=================================="

update_filial_data='{
    "nome": "Filial Atualizada",
    "logradouro": "Rua Atualizada",
    "numero": "456",
    "complemento": "Sala 2",
    "bairro": "Centro",
    "cidade": "Rio de Janeiro",
    "estado": "RJ",
    "cep": "22000000",
    "telefone": "(21) 88888-8888"
}'

update_moto_data='{
    "modelo": "Yamaha Atualizada",
    "ano": 2023,
    "cor": "Vermelha"
}'

test_endpoint "PUT" "http://localhost:5000/api/filial/1" "$update_filial_data" "Atualizar filial"
test_endpoint "PUT" "http://localhost:5000/api/moto/1" "$update_moto_data" "Atualizar moto"

echo -e "\n${YELLOW}📋 TESTANDO OPERAÇÕES ESPECIAIS${NC}"
echo "=================================="

test_endpoint "PATCH" "http://localhost:5000/api/filial/1/ativar" "" "Ativar filial"
test_endpoint "PATCH" "http://localhost:5000/api/filial/1/desativar" "" "Desativar filial"
test_endpoint "PATCH" "http://localhost:5000/api/moto/1/disponivel" "" "Marcar moto como disponível"
test_endpoint "PATCH" "http://localhost:5000/api/moto/1/indisponivel" "" "Marcar moto como indisponível"

echo -e "\n${YELLOW}📋 TESTANDO EXCLUSÃO DE DADOS${NC}"
echo "=================================="

test_endpoint "DELETE" "http://localhost:5000/api/moto/1" "" "Excluir moto"
test_endpoint "DELETE" "http://localhost:5000/api/filial/1" "" "Excluir filial"

echo -e "\n${GREEN}🎉 TESTES CONCLUÍDOS!${NC}"
echo "=============================================="
echo -e "${BLUE}📊 Resumo dos testes:${NC}"
echo "   • Endpoints de Filiais: 7 testes"
echo "   • Endpoints de Motos: 9 testes"
echo "   • Total: 16 endpoints testados"
echo ""
echo -e "${YELLOW}💡 Nota:${NC} Os testes podem falhar devido à falta de conexão com o Oracle."
echo -e "   Para testes completos, execute o script SQL no banco de dados."
echo ""
echo -e "${BLUE}🔗 Swagger UI:${NC} http://localhost:5000"
echo -e "${BLUE}📚 Documentação:${NC} Veja o README.md para instruções completas"
