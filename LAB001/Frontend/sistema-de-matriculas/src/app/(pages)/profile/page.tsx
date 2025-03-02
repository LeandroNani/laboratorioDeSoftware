'use client';

import { useState } from "react";
import { Aluno } from "@/types/aluno.type";
import { FiUser, FiBook } from "react-icons/fi";
import Navbar from "@/components/Navbar";

const PerfilPage = () => {
  const [selectedTab, setSelectedTab] = useState("perfil");
  const aluno: Aluno = {
    numeroDePessoa: 456,
    nome: "Aluno Exemplo",
    senha: "senha123",
    matricula: "20250001",
    email: "aluno@example.com",
    mensalidade: 1500,
    curso: {
      id: "1",
      nome: "Engenharia de Software",
      numeroDeCreditos: 180,
      alunos: [],
      disciplinas: [],
    },
    planoDeEnsino: [
      {
        id: "1",
        nome: "Algoritmos Avançados",
        isActive: true,
        preco: 500,
        periodo: "2025.1",
        campus: "Campus Principal",
        alunos: [],
        disciplinasNecessarias: [],
        professor: {
          numeroDePessoa: 123,
          nome: "Professor Example",
          senha: "password",
          disciplinas: [],
          nivelEscolar: "Doutor",
        },
      },
      {
        id: "2",
        nome: "Sistemas Distribuídos",
        isActive: true,
        preco: 600,
        periodo: "2025.1",
        campus: "Campus Principal",
        alunos: [],
        disciplinasNecessarias: [],
        professor: {
          numeroDePessoa: 123,
          nome: "Professor Example",
          senha: "password",
          disciplinas: [],
          nivelEscolar: "Doutor",
        },
      },
    ],
    disciplinasCursadas: [
      {
        id: "3",
        nome: "Introdução à Programação",
        isActive: true,
        preco: 400,
        periodo: "2024.2",
        campus: "Campus Principal",
        alunos: [],
        disciplinasNecessarias: [],
        professor: {
          numeroDePessoa: 124,
          nome: "Professor Example 2",
          senha: "password",
          disciplinas: [],
          nivelEscolar: "Mestre",
        },
      },
    ],
  };

  return (
    <>
      <Navbar />
      <div className="flex min-h-screen bg-black text-white">
        {/* Sidebar */}
        <aside className="w-72 bg-zinc-900 shadow-lg min-h-screen p-6 flex flex-col">
          <nav className="mt-6">
            <button
              className={`flex items-center gap-3 p-3 w-full rounded-lg text-lg transition ${
                selectedTab === "perfil"
                  ? "bg-white text-black font-semibold"
                  : "hover:bg-gray-700"
              }`}
              onClick={() => setSelectedTab("perfil")}
            >
              <FiUser size={20} /> Perfil
            </button>
            <button
              className={`flex items-center gap-3 p-3 w-full rounded-lg text-lg mt-2 transition ${
                selectedTab === "disciplinas"
                  ? "bg-white text-black font-semibold"
                  : "hover:bg-zinc-700"
              }`}
              onClick={() => setSelectedTab("disciplinas")}
            >
              <FiBook size={20} /> Disciplinas
            </button>
          </nav>
        </aside>

        {/* Conteúdo principal */}
        <main className="flex-1 p-10">
          {selectedTab === "perfil" ? (
            <div className="bg-zinc-900 shadow-lg rounded-lg p-6 max-w-3xl mx-auto">
              <div className="mt-4 space-y-4">
                <p className="text-lg text-gray-300">
                  <strong className="text-white">Nome:</strong> {aluno.nome}
                </p>
                <p className="text-lg text-gray-300">
                  <strong className="text-white">Matrícula:</strong> {aluno.matricula}
                </p>
                <p className="text-lg text-gray-300">
                  <strong className="text-white">Email:</strong> {aluno.email}
                </p>
                <p className="text-lg text-gray-300">
                  <strong className="text-white">Curso:</strong> {aluno.curso?.nome}
                </p>
                <p className="text-lg text-gray-300">
                  <strong className="text-white">Mensalidade:</strong> R$ {aluno.mensalidade}
                </p>
              </div>
            </div>
          ) : (
            <div>
              {/* Disciplinas Matriculadas */}
              <div className="bg-zinc-800 shadow-lg rounded-lg p-6 max-w-3xl mx-auto">
                <h2 className="text-xl font-semibold text-white">
                  Disciplinas Matriculadas
                </h2>
                <ul className="mt-4 space-y-4">
                  {aluno.planoDeEnsino.map((disciplina) => (
                    <li
                      key={disciplina.id}
                      className="border border-gray-600 rounded-lg p-4 bg-gray-900 shadow-sm"
                    >
                      <p className="text-lg font-semibold text-white">{disciplina.nome}</p>
                      <p className="text-sm text-gray-300">
                        <strong className="text-white">Período:</strong> {disciplina.periodo}
                      </p>
                      <p className="text-sm text-gray-300">
                        <strong className="text-white">Campus:</strong> {disciplina.campus}
                      </p>
                      <p className="text-sm text-gray-300">
                        <strong className="text-white">Professor:</strong> {disciplina.professor.nome} (
                        {disciplina.professor.nivelEscolar})
                      </p>
                    </li>
                  ))}
                </ul>
              </div>

              {/* Disciplinas Cursadas */}
              <div className="bg-zinc-800 shadow-lg rounded-lg p-6 max-w-3xl mx-auto mt-6">
                <h2 className="text-xl font-semibold text-white">
                  Disciplinas Cursadas
                </h2>
                <ul className="mt-4 space-y-4">
                  {aluno.disciplinasCursadas.map((disciplina) => (
                    <li
                      key={disciplina.id}
                      className="border border-gray-600 rounded-lg p-4 bg-gray-900 shadow-sm"
                    >
                      <p className="text-lg font-semibold text-white">{disciplina.nome}</p>
                      <p className="text-sm text-gray-300">
                        <strong className="text-white">Período:</strong> {disciplina.periodo}
                      </p>
                      <p className="text-sm text-gray-300">
                        <strong className="text-white">Campus:</strong> {disciplina.campus}
                      </p>
                      <p className="text-sm text-gray-300">
                        <strong className="text-white">Professor:</strong> {disciplina.professor.nome} (
                        {disciplina.professor.nivelEscolar})
                      </p>
                    </li>
                  ))}
                </ul>
              </div>
            </div>
          )}
        </main>
      </div>
    </>
  );
};

export default PerfilPage;