"use client";

import React, { useEffect, useState } from "react";

// Tipos mockados
type Agente = {
  id: number;
  nome: string;
  email: string;
  cnpj: string;
  endereco: string;
  quantidadeCarros: number;
};

type Automovel = {
  id: number;
  matricula: number;
  ano: number;
  marca: string;
  modelo: string;
  placa: string;
};

type Contrato = {
  id: number;
  clienteNome: string;
  automovel: Automovel;
  tipoContrato: string;
};

export default function AdminPage() {
  const [selectedTab, setSelectedTab] = useState("criarAgente");

  const [formData, setFormData] = useState({
    nome: "",
    email: "",
    senha: "",
    cnpj: "",
    endereco: "",
    quantidadeCarros: 0,
  });

  const [autoForm, setAutoForm] = useState({
    matricula: "",
    ano: "",
    marca: "",
    modelo: "",
    placa: "",
    agenteId: 0,
  });

  const [message, setMessage] = useState("");
  const [agentes, setAgentes] = useState<Agente[]>([]);
  const [contratos, setContratos] = useState<Contrato[]>([]);

  useEffect(() => {
    // Mock agentes
    setAgentes([
      {
        id: 1,
        nome: "Banco ABC",
        email: "abc@banco.com",
        cnpj: "12.345.678/0001-99",
        endereco: "Av. Central, 456",
        quantidadeCarros: 10,
      },
      {
        id: 2,
        nome: "Banco XYZ",
        email: "xyz@banco.com",
        cnpj: "98.765.432/0001-00",
        endereco: "Av. Norte, 789",
        quantidadeCarros: 15,
      },
    ]);

    // Mock contratos
    setContratos([
      {
        id: 2,
        clienteNome: "João da Silva",
        tipoContrato: "debito",
        automovel: {
          id: 2,
          matricula: 102,
          ano: 2019,
          marca: "Honda",
          modelo: "Civic",
          placa: "XYZ-5678",
        },
      },
    ]);
  }, []);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleAutoChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
    setAutoForm({ ...autoForm, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setMessage("");

    const token = localStorage.getItem("token");

    try {
      const response = await fetch("http://localhost:5145/api/Admin/criar-agente", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify({
          nome: formData.nome,
          email: formData.email,
          senha: formData.senha,
          cnpj: formData.cnpj,
          endereco: formData.endereco,
          quantidadeCarros: Number(formData.quantidadeCarros),
        }),
      });

      const result = await response.text();
      if (response.ok) {
        setMessage("✅ " + result);
        setFormData({ nome: "", email: "", senha: "", cnpj: "", endereco: "", quantidadeCarros: 0 });
      } else {
        setMessage("❌ " + result);
      }
    } catch (err) {
      setMessage("Erro ao enviar requisição.");
      console.error(err);
    }
  };

  return (
    <div className="flex min-h-screen bg-gray-100 text-black">
      {/* Sidebar */}
      <aside className="w-72 bg-gray-800 p-6 text-white shadow-lg">
        <nav className="flex flex-col gap-2">
          {[
            { key: "criarAgente", label: "🏢 Criar Agente" },
            { key: "verAgentes", label: "👥 Ver Agentes" },
            { key: "cadastrarCarro", label: "🚗 Cadastrar Automóvel" },
            { key: "contratos", label: "📑 Contratos Ativos" },
          ].map((tab) => (
            <button
              key={tab.key}
              onClick={() => setSelectedTab(tab.key)}
              className={`p-3 rounded-lg text-left text-lg transition ${
                selectedTab === tab.key ? "bg-white text-black font-semibold" : "hover:bg-gray-700"
              }`}
            >
              {tab.label}
            </button>
          ))}
        </nav>
      </aside>

      {/* Conteúdo principal */}
      <main className="flex-1 p-10">
        {selectedTab === "criarAgente" && (
          <div className="bg-white shadow-lg rounded-lg p-6 max-w-xl mx-auto">
            <h2 className="text-xl font-semibold mb-4">Cadastrar Novo Agente</h2>
            <form onSubmit={handleSubmit} className="space-y-4">
              <input type="text" name="nome" placeholder="Nome" value={formData.nome} onChange={handleChange} className="w-full px-3 py-2 border rounded" required />
              <input type="email" name="email" placeholder="Email" value={formData.email} onChange={handleChange} className="w-full px-3 py-2 border rounded" required />
              <input type="password" name="senha" placeholder="Senha" value={formData.senha} onChange={handleChange} className="w-full px-3 py-2 border rounded" required />
              <input type="text" name="cnpj" placeholder="CNPJ" value={formData.cnpj} onChange={handleChange} className="w-full px-3 py-2 border rounded" required />
              <input type="text" name="endereco" placeholder="Endereço" value={formData.endereco} onChange={handleChange} className="w-full px-3 py-2 border rounded" />
              <input type="number" name="quantidadeCarros" placeholder="Qtd. Carros" value={formData.quantidadeCarros} onChange={handleChange} className="w-full px-3 py-2 border rounded" required />
              <button type="submit" className="w-full bg-blue-600 text-white py-2 rounded hover:bg-blue-700">Criar Agente</button>
              {message && <p className={`text-sm mt-2 ${message.startsWith("✅") ? "text-green-600" : "text-red-500"}`}>{message}</p>}
            </form>
          </div>
        )}

        {selectedTab === "verAgentes" && (
          <div className="bg-white shadow-lg rounded-lg p-6 max-w-4xl mx-auto">
            <h2 className="text-xl font-semibold mb-4">Agentes Cadastrados</h2>
            <ul className="space-y-4">
              {agentes.map((a) => (
                <li key={a.id} className="border p-4 rounded bg-gray-50 shadow-sm">
                  <p><strong>Nome:</strong> {a.nome}</p>
                  <p><strong>Email:</strong> {a.email}</p>
                  <p><strong>CNPJ:</strong> {a.cnpj}</p>
                  <p><strong>Endereço:</strong> {a.endereco}</p>
                  <p><strong>Quantidade de Carros:</strong> {a.quantidadeCarros}</p>
                </li>
              ))}
            </ul>
          </div>
        )}

        {selectedTab === "cadastrarCarro" && (
          <div className="bg-white shadow-lg rounded-lg p-6 max-w-xl mx-auto">
            <h2 className="text-xl font-semibold mb-4">Cadastrar Novo Automóvel</h2>
            <form className="space-y-4">
              <input type="text" name="matricula" placeholder="Matrícula" value={autoForm.matricula} onChange={handleAutoChange} className="w-full px-3 py-2 border rounded" />
              <input type="text" name="ano" placeholder="Ano" value={autoForm.ano} onChange={handleAutoChange} className="w-full px-3 py-2 border rounded" />
              <input type="text" name="marca" placeholder="Marca" value={autoForm.marca} onChange={handleAutoChange} className="w-full px-3 py-2 border rounded" />
              <input type="text" name="modelo" placeholder="Modelo" value={autoForm.modelo} onChange={handleAutoChange} className="w-full px-3 py-2 border rounded" />
              <input type="text" name="placa" placeholder="Placa" value={autoForm.placa} onChange={handleAutoChange} className="w-full px-3 py-2 border rounded" />
              <select name="agenteId" value={autoForm.agenteId} onChange={handleAutoChange} className="w-full px-3 py-2 border rounded">
                <option value="">Selecione o Agente</option>
                {agentes.map((a) => (
                  <option key={a.id} value={a.id}>{a.nome}</option>
                ))}
              </select>
              <button type="submit" className="w-full bg-green-600 text-white py-2 rounded hover:bg-green-700">Cadastrar Automóvel</button>
            </form>
          </div>
        )}

        {selectedTab === "contratos" && (
          <div className="bg-white shadow-lg rounded-lg p-6 max-w-4xl mx-auto">
            <h2 className="text-xl font-semibold mb-4">Contratos Ativos</h2>
            <ul className="space-y-4">
              {contratos.map((c) => (
                <li key={c.id} className="border p-4 rounded bg-gray-50 shadow-sm">
                  <p><strong>Cliente:</strong> {c.clienteNome}</p>
                  <p><strong>Automóvel:</strong> {c.automovel.marca} {c.automovel.modelo}</p>
                  <p><strong>Placa:</strong> {c.automovel.placa}</p>
                  <p><strong>Tipo de Contrato:</strong> {c.tipoContrato}</p>
                </li>
              ))}
            </ul>
          </div>
        )}
      </main>
    </div>
  );
}
