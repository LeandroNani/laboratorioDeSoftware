"use client";

import React, { useState, useEffect } from "react";

// Tipos de dados
type Automovel = {
matricula: number;
ano: number;
marca: string;
modelo: string;
placa: string;
quantidade: number;
};

type Pedido = {
id: number;
contratante: Cliente;
agenteDesignado: Agente;
status: boolean;
automovel: Automovel;
};

type Cliente = {
rg: string;
cpf: string;
nome: string;
endereco: string;
profissao: string;
};

type Agente = {
cnpj: string;
nome: string;
endereco: string;
quantidadeCarros: number;
};

export default function ClientePage() {
const [selectedTab, setSelectedTab] = useState("perfil");
const [cliente, setCliente] = useState<Cliente | null>();
const [automoveis, setAutomoveis] = useState<Automovel[]>([]);
const [pedidos, setPedidos] = useState<Pedido[]>([]);
const [contratos, setContratos] = useState<Pedido[]>([]);

// Fetch inicial para carregar os dados
useEffect(() => {
  fetchCliente();
  fetchAutomoveis();
  fetchPedidos();
  fetchContratos();
}, []);

// Funções para buscar dados (simulação com mock enquanto o backend não está pronto)
const fetchCliente = async () => {
  // Simulando fetch do cliente
  const mockCliente: Cliente = {
    rg: "123456789",
    cpf: "111.222.333-44",
    nome: "João da Silva",
    endereco: "Rua das Flores, 123",
    profissao: "Engenheiro",
  };
  setCliente(mockCliente);
};

const fetchAutomoveis = async () => {
  // Simulando fetch de automóveis
  const mockAutomoveis: Automovel[] = [
    {
      matricula: 1,
      ano: 2020,
      marca: "Toyota",
      modelo: "Corolla",
      placa: "ABC-1234",
      quantidade: 5,
    },
    {
      matricula: 2,
      ano: 2019,
      marca: "Honda",
      modelo: "Civic",
      placa: "XYZ-5678",
      quantidade: 3,
    },
  ];
  setAutomoveis(mockAutomoveis);
};

const fetchPedidos = async () => {
  // Simulando fetch de pedidos
  const mockPedidos: Pedido[] = [
    {
      id: 1,
      contratante: {
        rg: "123456789",
        cpf: "111.222.333-44",
        nome: "João da Silva",
        endereco: "Rua das Flores, 123",
        profissao: "Engenheiro",
      },
      agenteDesignado: {
        cnpj: "12.345.678/0001-99",
        nome: "Banco ABC",
        endereco: "Av. Central, 456",
        quantidadeCarros: 10,
      },
      status: true,
      automovel: {
        matricula: 1,
        ano: 2020,
        marca: "Toyota",
        modelo: "Corolla",
        placa: "ABC-1234",
        quantidade: 5,
      },
    },
  ];
  setPedidos(mockPedidos);
};

const fetchContratos = async () => {
  // Simulando fetch de contratos
  const mockContratos: Pedido[] = [
    {
      id: 2,
      contratante: {
        rg: "123456789",
        cpf: "111.222.333-44",
        nome: "João da Silva",
        endereco: "Rua das Flores, 123",
        profissao: "Engenheiro",
      },
      agenteDesignado: {
        cnpj: "12.345.678/0001-99",
        nome: "Banco XYZ",
        endereco: "Av. Norte, 789",
        quantidadeCarros: 15,
      },
      status: true,
      automovel: {
        matricula: 2,
        ano: 2019,
        marca: "Honda",
        modelo: "Civic",
        placa: "XYZ-5678",
        quantidade: 3,
      },
    },
  ];
  setContratos(mockContratos);
};

return (
  <div className="flex min-h-screen bg-gray-100 text-black">
    {/* Sidebar */}
    <aside className="w-72 bg-gray-800 shadow-lg min-h-screen p-6 flex flex-col text-white">
      <nav className="mt-6">
        <button
          className={`flex items-center gap-3 p-3 w-full rounded-lg text-lg transition ${
            selectedTab === "perfil"
              ? "bg-white text-black font-semibold"
              : "hover:bg-gray-700"
          }`}
          onClick={() => setSelectedTab("perfil")}
        >
          👤 Perfil
        </button>
        <button
          className={`flex items-center gap-3 p-3 w-full rounded-lg text-lg mt-2 transition ${
            selectedTab === "pedidos"
              ? "bg-white text-black font-semibold"
              : "hover:bg-gray-700"
          }`}
          onClick={() => setSelectedTab("pedidos")}
        >
          📋 Pedidos de Aluguel
        </button>
        <button
          className={`flex items-center gap-3 p-3 w-full rounded-lg text-lg mt-2 transition ${
            selectedTab === "automoveis"
              ? "bg-white text-black font-semibold"
              : "hover:bg-gray-700"
          }`}
          onClick={() => setSelectedTab("automoveis")}
        >
          🚗 Automóveis Disponíveis
        </button>
        <button
          className={`flex items-center gap-3 p-3 w-full rounded-lg text-lg mt-2 transition ${
            selectedTab === "contratos"
              ? "bg-white text-black font-semibold"
              : "hover:bg-gray-700"
          }`}
          onClick={() => setSelectedTab("contratos")}
        >
          ✅ Contratos Ativos
        </button>
      </nav>
    </aside>

    {/* Conteúdo principal */}
    <main className="flex-1 p-10">
      {selectedTab === "perfil" && cliente && (
        <div className="bg-white shadow-lg rounded-lg p-6 max-w-3xl mx-auto">
          <h2 className="text-xl font-semibold mb-4">Perfil</h2>
          <p className="text-lg text-gray-600">
            <strong>Nome:</strong> {cliente.nome}
          </p>
          <p className="text-lg text-gray-600">
            <strong>CPF:</strong> {cliente.cpf}
          </p>
          <p className="text-lg text-gray-600">
            <strong>Endereço:</strong> {cliente.endereco}
          </p>
          <p className="text-lg text-gray-600">
            <strong>Profissão:</strong> {cliente.profissao}
          </p>
        </div>
      )}

      {selectedTab === "pedidos" && (
        <div className="bg-white shadow-lg rounded-lg p-6 max-w-3xl mx-auto">
          <h2 className="text-xl font-semibold mb-4">Pedidos de Aluguel</h2>
          <ul className="mt-4 space-y-4">
            {pedidos.map((pedido) => (
              <li key={pedido.id} className="border border-gray-300 rounded-lg p-4 bg-gray-50 shadow-sm">
                <p className="text-lg font-semibold">Pedido #{pedido.id}</p>
                <p className="text-sm text-gray-600">
                  <strong>Status:</strong> {pedido.status ? "Aprovado" : "Em análise"}
                </p>
                <p className="text-sm text-gray-600">
                  <strong>Automóvel:</strong> {pedido.automovel.marca} {pedido.automovel.modelo}
                </p>
              </li>
            ))}
          </ul>
        </div>
      )}

      {selectedTab === "automoveis" && (
        <div className="bg-white shadow-lg rounded-lg p-6 max-w-3xl mx-auto">
          <h2 className="text-xl font-semibold mb-4">Automóveis Disponíveis</h2>
          <ul className="mt-4 space-y-4">
            {automoveis.map((automovel) => (
              <li key={automovel.matricula} className="border border-gray-300 rounded-lg p-4 bg-gray-50 shadow-sm">
                <p className="text-lg font-semibold">{automovel.marca} {automovel.modelo}</p>
                <p className="text-sm text-gray-600">
                  <strong>Ano:</strong> {automovel.ano}
                </p>
                <p className="text-sm text-gray-600">
                  <strong>Placa:</strong> {automovel.placa}
                </p>
              </li>
            ))}
          </ul>
        </div>
      )}

      {selectedTab === "contratos" && (
        <div className="bg-white shadow-lg rounded-lg p-6 max-w-3xl mx-auto">
          <h2 className="text-xl font-semibold mb-4">Contratos Ativos</h2>
          <ul className="mt-4 space-y-4">
            {contratos.map((contrato) => (
              <li key={contrato.id} className="border border-gray-300 rounded-lg p-4 bg-gray-50 shadow-sm">
                <p className="text-lg font-semibold">Contrato #{contrato.id}</p>
                <p className="text-sm text-gray-600">
                  <strong>Automóvel:</strong> {contrato.automovel.marca} {contrato.automovel.modelo}
                </p>
              </li>
            ))}
          </ul>
        </div>
      )}
    </main>
  </div>
);
}