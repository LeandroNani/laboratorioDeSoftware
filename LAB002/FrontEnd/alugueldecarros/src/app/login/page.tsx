"use client";

import { useState } from "react";
import { useRouter } from "next/navigation";
import API_BASE_URL from "@/api/api";

export default function Login() {
  const router = useRouter();
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [tipoUsuario, setTipoUsuario] = useState<"admin" | "cliente" | "agente" | "">("");
  const [error, setError] = useState("");

  const handleLogin = async (e: React.FormEvent) => {
    e.preventDefault();
    setError("");

    if (!tipoUsuario) {
      setError("Selecione o tipo de usuário.");
      return;
    }

    try {
      const response = await fetch(`${API_BASE_URL}/auth/login`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          email,
          senha: password,
          tipoUsuario,
        }),
      });

      if (response.ok) {
        const data = await response.json();

        localStorage.setItem("token", data.token);
        localStorage.setItem("tipoUsuario", data.tipoUsuario);
        localStorage.setItem("usuarioId", data.usuarioId.toString());

        router.push(`/${data.tipoUsuario}`);
      } else {
        const text = await response.text();
        setError(text || "Erro ao fazer login.");
      }
    } catch (err) {
      setError("Erro de rede. Verifique sua conexão.");
      console.error(err);
    }
  };

  return (
    <div className="flex items-center justify-center min-h-screen bg-gray-100">
      <div className="bg-white p-6 rounded shadow-md w-full max-w-sm">
        <h1 className="text-xl text-gray-700 font-bold mb-4">Login</h1>
        <form onSubmit={handleLogin}>
          <div className="mb-4">
            <label className="block text-sm font-medium text-gray-700 mb-1">Tipo de Usuário</label>
            <div className="flex gap-2 mb-2">
              {["admin", "cliente", "agente"].map((tipo) => (
                <button
                  type="button"
                  key={tipo}
                  onClick={() => setTipoUsuario(tipo as any)}
                  className={`flex-1 px-3 py-1 rounded border ${
                    tipoUsuario === tipo
                      ? "bg-blue-500 text-white"
                      : "bg-gray-100 text-gray-700"
                  }`}
                >
                  {tipo.charAt(0).toUpperCase() + tipo.slice(1)}
                </button>
              ))}
            </div>
          </div>

          <div className="mb-4">
            <label htmlFor="email" className="block text-sm font-medium text-gray-700">
              Email
            </label>
            <input
              type="email"
              id="email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              required
              className="w-full px-3 py-2 border rounded"
            />
          </div>

          <div className="mb-4">
            <label htmlFor="password" className="block text-sm font-medium text-gray-700">
              Senha
            </label>
            <input
              type="password"
              id="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              required
              className="w-full px-3 py-2 border rounded"
            />
          </div>

          {error && (
            <div className="mb-4 text-red-500 text-sm text-center">{error}</div>
          )}

          <button
            type="submit"
            className="w-full bg-blue-500 text-white py-2 rounded hover:bg-blue-600"
          >
            Entrar
          </button>
        </form>

        <p className="mt-4 text-gray-700 text-sm text-center">
          Não tem uma conta?{" "}
          <a href="/register" className="text-blue-500 underline">
            Registre-se
          </a>
        </p>
      </div>
    </div>
  );
}
