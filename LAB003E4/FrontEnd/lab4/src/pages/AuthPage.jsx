"use client";

import { useState } from "react";
import { useNavigate } from "react-router-dom"; // se estiver usando react-router-dom
// ou useRouter se estiver no Next.js
// import { useRouter } from "next/navigation";

const API_BASE_URL = "http://localhost:3000"; // ajuste conforme sua API

export default function AuthPage() {
  const navigate = useNavigate(); // ou const router = useRouter();
  const [modo, setModo] = useState("login");
  const [step, setStep] = useState(1);
  const [error, setError] = useState("");

  const [form, setForm] = useState({
    nome: "",
    email: "",
    senha: "",
    rg: "",
    cpf: "",
    endereco: "",
    instituicao: "",
    curso: "",
  });

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleLogin = async (e) => {
    e.preventDefault();
    setError("");

    try {
      const response = await fetch(`${API_BASE_URL}/auth/login`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          email: form.email,
          senha: form.senha,
        }),
      });

      if (response.ok) {
        const data = await response.json();
        localStorage.setItem("token", data.token);
        localStorage.setItem("usuarioId", data.usuarioId);
        navigate("/dashboard"); // ou router.push("/dashboard")
      } else {
        const text = await response.text();
        setError(text || "Erro ao fazer login.");
      }
    } catch {
      setError("Erro de rede.");
    }
  };

  const handleCadastro = async (e) => {
    e.preventDefault();
    try {
      const response = await fetch(`${API_BASE_URL}/alunos/registrar`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(form),
      });

      if (response.ok) {
        alert("Cadastro realizado com sucesso!");
        setModo("login");
        setStep(1);
      } else {
        const text = await response.text();
        setError(text || "Erro ao cadastrar.");
      }
    } catch {
      setError("Erro de rede.");
    }
  };

  return (
    
    <div className="flex items-center justify-center min-h-screen bg-gray-100 px-4">
      <div className="bg-white p-6 rounded shadow-md w-full max-w-sm">
        <h1 className="text-xl font-bold mb-4 text-gray-700 text-center">
          {modo === "login" ? "Login" : `Cadastro - Etapa ${step}`}
        </h1>

        <form onSubmit={modo === "login" ? handleLogin : step === 1 ? (e) => { e.preventDefault(); setStep(2); } : handleCadastro}>
          {modo === "login" ? (
            <>
              <div className="mb-4">
                <label className="block text-sm font-medium text-gray-700">Email</label>
                <input
                  type="email"
                  name="email"
                  value={form.email}
                  onChange={handleChange}
                  required
                  className="w-full px-3 py-2 border rounded"
                />
              </div>

              <div className="mb-4">
                <label className="block text-sm font-medium text-gray-700">Senha</label>
                <input
                  type="password"
                  name="senha"
                  value={form.senha}
                  onChange={handleChange}
                  required
                  className="w-full px-3 py-2 border rounded"
                />
              </div>
            </>
          ) : step === 1 ? (
            <>
              <input type="text" name="nome" placeholder="Nome" value={form.nome} onChange={handleChange} required className="w-full px-3 py-2 border rounded mb-3" />
              <input type="email" name="email" placeholder="Email" value={form.email} onChange={handleChange} required className="w-full px-3 py-2 border rounded mb-3" />
              <input type="password" name="senha" placeholder="Senha" value={form.senha} onChange={handleChange} required className="w-full px-3 py-2 border rounded mb-3" />
            </>
          ) : (
            <>
              {["rg", "cpf", "endereco", "instituicao", "curso"].map((field) => (
                <input
                  key={field}
                  type="text"
                  name={field}
                  placeholder={field.charAt(0).toUpperCase() + field.slice(1)}
                  value={form[field]}
                  onChange={handleChange}
                  required
                  className="w-full px-3 py-2 border rounded mb-3"
                />
              ))}
            </>
          )}

          {error && <p className="text-red-500 text-sm mb-3 text-center">{error}</p>}

          <button className="w-full bg-blue-500 text-white py-2 rounded hover:bg-blue-600">
            {modo === "login" ? "Entrar" : step === 1 ? "Próxima Etapa" : "Finalizar Cadastro"}
          </button>
        </form>

        <p className="mt-4 text-sm text-gray-700 text-center">
          {modo === "login" ? "Não tem conta?" : "Já tem conta?"}{" "}
          <button className="text-blue-500 underline" onClick={() => { setModo(modo === "login" ? "cadastro" : "login"); setStep(1); setError(""); }}>
            {modo === "login" ? "Cadastre-se" : "Faça login"}
          </button>
        </p>
      </div>
    </div>
  );
}
