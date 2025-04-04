"use client";

import { useState } from "react";
import API_BASE_URL from "@/api/api";

export default function Register() {
  const [step, setStep] = useState(1);
  const [formData, setFormData] = useState({
    nome: "",
    email: "",
    senha: "",
    rg: "",
    cpf: "",
    endereco: "",
    profissao: "",
    entidadeEmpregadora: "",
  });

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleNext = (e: React.FormEvent) => {
    e.preventDefault();
    setStep(2);
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    try {
      const response = await fetch(`${API_BASE_URL}/clientes/registrar`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(formData),
      });

      if (response.ok) {
        alert("Registro realizado com sucesso!");
      } else {
        alert("Erro ao registrar.");
      }
    } catch (error) {
      console.error("Erro ao registrar:", error);
    }
  };

  return (
    <div className="flex items-center justify-center min-h-screen bg-gray-100">
      <div className="bg-white p-6 rounded shadow-md w-full max-w-md">
        <h1 className="text-xl font-bold mb-4 text-gray-700">
          Registro - Etapa {step}
        </h1>

        {step === 1 ? (
          <form onSubmit={handleNext}>
            {["nome", "email", "senha"].map((field) => (
              <div className="mb-4" key={field}>
                <label
                  htmlFor={field}
                  className="block text-sm font-medium text-gray-700"
                >
                  {field.charAt(0).toUpperCase() + field.slice(1)}
                </label>
                <input
                  type={field === "senha" ? "password" : "text"}
                  id={field}
                  name={field}
                  value={(formData as any)[field]}
                  onChange={handleChange}
                  required
                  className="w-full px-3 py-2 border rounded"
                />
              </div>
            ))}
            <button
              type="submit"
              className="w-full bg-blue-500 text-white py-2 rounded hover:bg-blue-600"
            >
              Próxima Etapa
            </button>
          </form>
        ) : (
          <form onSubmit={handleSubmit}>
            {["rg", "cpf", "endereco", "profissao", "entidadeEmpregadora"].map(
              (field) => (
                <div className="mb-4" key={field}>
                  <label
                    htmlFor={field}
                    className="block text-sm font-medium text-gray-700"
                  >
                    {field.charAt(0).toUpperCase() + field.slice(1)}
                  </label>
                  <input
                    type="text"
                    id={field}
                    name={field}
                    value={(formData as any)[field]}
                    onChange={handleChange}
                    required
                    className="w-full px-3 py-2 border rounded"
                  />
                </div>
              )
            )}
            <button
              type="submit"
              className="w-full bg-green-500 text-white py-2 rounded hover:bg-green-600"
            >
              Finalizar Registro
            </button>
          </form>
        )}
      </div>
    </div>
  );
}
