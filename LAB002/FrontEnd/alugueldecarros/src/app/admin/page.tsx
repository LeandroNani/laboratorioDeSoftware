"use client";

import React, { useEffect, useState } from "react";

// Tipos

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
  const [formData, setFormData] = useState({ nome: "", email: "", senha: "", cnpj: "", endereco: "", quantidadeCarros: 0 });
  const [autoForm, setAutoForm] = useState({ ano: "", marca: "", modelo: "", placa: "", agenteId: 0 });
  const [message, setMessage] = useState("");
  const [agentes, setAgentes] = useState<Agente[]>([]);
  const [contratos, setContratos] = useState<Contrato[]>([]);

  const API_BASE = "http://localhost:5145";
  const token = typeof window !== "undefined" ? localStorage.getItem("token") : null;

  useEffect(() => {
    fetchAgentes();
    fetchContratos();
  }, []);

  const fetchAgentes = async () => {
    try {
      const res = await fetch(`${API_BASE}/api/agentes`, {
        headers: { Authorization: `Bearer ${token}` },
      });
      const data = await res.json();
      setAgentes(data);
    } catch (err) {
      console.error("Erro ao buscar agentes:", err);
    }
  };

  const fetchContratos = async () => {
    try {
      const res = await fetch(`${API_BASE}/api/pedidos/contratos`, {
        headers: { Authorization: `Bearer ${token}` },
      });
      const data = await res.json();
      setContratos(data);
    } catch (err) {
      console.error("Erro ao buscar contratos:", err);
    }
  };

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleAutoChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
    setAutoForm({ ...autoForm, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setMessage("");
    try {
      const response = await fetch(`${API_BASE}/api/Admin/criar-agente`, {
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
        fetchAgentes();
      } else {
        setMessage("❌ " + result);
      }
    } catch (err) {
      setMessage("Erro ao enviar requisição.");
      console.error(err);
    }
  };

  const handleAutoSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setMessage("");
    try {
      const res = await fetch(`${API_BASE}/api/automoveis`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify({
          ano: parseInt(autoForm.ano),
          marca: autoForm.marca,
          modelo: autoForm.modelo,
          placa: autoForm.placa,
          fotoUrl: null,
          agenteId: Number(autoForm.agenteId),
        }),
      });
      const text = await res.text();
      setMessage(res.ok ? "✅ " + text : "❌ " + text);
    } catch (err) {
      console.error(err);
      setMessage("Erro ao cadastrar automóvel.");
    }
  };

  return (
    <div className="flex min-h-screen bg-gray-100 text-black">
      <aside className="w-72 bg-gray-800 p-6 text-white shadow-lg">
        <nav className="flex flex-col gap-2">
          {[{ key: "criarAgente", label: "🏢 Criar Agente" }, { key: "verAgentes", label: "👥 Ver Agentes" }, { key: "cadastrarCarro", label: "🚗 Cadastrar Automóvel" }, { key: "contratos", label: "📑 Contratos Ativos" }].map((tab) => (
            <button key={tab.key} onClick={() => setSelectedTab(tab.key)} className={`p-3 rounded-lg text-left text-lg transition ${selectedTab === tab.key ? "bg-white text-black font-semibold" : "hover:bg-gray-700"}`}>{tab.label}</button>
          ))}
        </nav>
      </aside>

      <main className="flex-1 p-10">
        {selectedTab === "criarAgente" && (
          <div className="bg-white shadow-lg rounded-lg p-6 max-w-xl mx-auto">
            <h2 className="text-xl font-semibold mb-4">Cadastrar Novo Agente</h2>
            <form onSubmit={handleSubmit} className="space-y-4">
              <input name="nome" placeholder="Nome" value={formData.nome} onChange={handleChange} className="w-full px-3 py-2 border rounded" required />
              <input name="email" type="email" placeholder="Email" value={formData.email} onChange={handleChange} className="w-full px-3 py-2 border rounded" required />
              <input name="senha" type="password" placeholder="Senha" value={formData.senha} onChange={handleChange} className="w-full px-3 py-2 border rounded" required />
              <input name="cnpj" placeholder="CNPJ" value={formData.cnpj} onChange={handleChange} className="w-full px-3 py-2 border rounded" required />
              <input name="endereco" placeholder="Endereço" value={formData.endereco} onChange={handleChange} className="w-full px-3 py-2 border rounded" />
              <input name="quantidadeCarros" type="number" placeholder="Qtd. Carros" value={formData.quantidadeCarros} onChange={handleChange} className="w-full px-3 py-2 border rounded" required />
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
            <form onSubmit={handleAutoSubmit} className="space-y-4">
              <input name="ano" placeholder="Ano" value={autoForm.ano} onChange={handleAutoChange} className="w-full px-3 py-2 border rounded" />
              <input name="marca" placeholder="Marca" value={autoForm.marca} onChange={handleAutoChange} className="w-full px-3 py-2 border rounded" />
              <input name="modelo" placeholder="Modelo" value={autoForm.modelo} onChange={handleAutoChange} className="w-full px-3 py-2 border rounded" />
              <input name="placa" placeholder="Placa" value={autoForm.placa} onChange={handleAutoChange} className="w-full px-3 py-2 border rounded" />
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