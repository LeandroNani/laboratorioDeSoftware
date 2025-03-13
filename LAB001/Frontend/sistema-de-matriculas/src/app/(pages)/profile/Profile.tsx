'use client';

import React from "react";
import { useState } from "react";
import { Aluno } from "@/@types/aluno.type";
import { FiUser, FiBook, FiLayers, FiHelpCircle } from "react-icons/fi";
import Navbar from "@/components/Navbar";
import { toast } from "react-toastify";
import { Disciplina } from "@/@types/disciplina.type";
import { efetuarMatricula, updateAluno } from "@/api/admin";
import { OverlayTrigger, ToastContainer, Tooltip } from "react-bootstrap";

export default function Page(aluno: Aluno) {
  const [selectedTab, setSelectedTab] = useState("perfil");
  const [selectedDisciplinas, setSelectedDisciplinas] = useState<Disciplina[]>([]);

  const handleSelectDisciplina = (disciplina: Disciplina) => {
    setSelectedDisciplinas((prevSelected) =>
      prevSelected.some((d) => d.id === disciplina.id)
        ? prevSelected.filter((d) => d.id !== disciplina.id)
        : [...prevSelected, disciplina]
    );
  };

  const handleConfirmSelection = () => {
    aluno.matricula.planoDeEnsino = selectedDisciplinas;
    toast.success(`Matricula efetuada`, {
      position: "top-right",
      autoClose: 3000,
      theme: "colored",
    })
    efetuarMatricula(aluno);
  };

  const handlePagarMatricula = () => {
    aluno.matricula.paga = true;
    aluno.matricula.ativa = true;
    if(aluno.matricula.planoDeEnsino === null) {
      aluno.matricula.planoDeEnsino = [];
    }
    toast.success(`Pagamento efetuado`, {
      position: "top-right",
      autoClose: 3000,
      theme: "colored",
    })
    updateAluno(aluno)
  };

  return (
    <>
      <ToastContainer />
      <Navbar />
      {!aluno.matricula.paga && (
        <div className="w-full bg-red-600 text-white text-center py-2">
          Pagamento de matricula pendente
        </div>
      )}
      <div className="flex min-h-screen bg-black text-white">
        {/* Sidebar */}
        <aside className="w-72 bg-zinc-900 shadow-lg min-h-screen p-6 flex flex-col">
          <nav className="mt-6">
            <button
              className={`flex items-center gap-3 p-3 w-full rounded-lg text-lg transition ${selectedTab === "perfil"
                ? "bg-white text-black font-semibold"
                : "hover:bg-gray-700"
                }`}
              onClick={() => setSelectedTab("perfil")}
            >
              <FiUser size={20} /> Perfil
            </button>
            <button
              className={`flex items-center gap-3 p-3 w-full rounded-lg text-lg mt-2 transition ${selectedTab === "disciplinas"
                ? "bg-white text-black font-semibold"
                : "hover:bg-zinc-700"
                }`}
              onClick={() => setSelectedTab("disciplinas")}
            >
              <FiBook size={20} /> Disciplinas
            </button>
            <button
              className={`flex items-center gap-3 p-3 w-full rounded-lg text-lg mt-2 transition ${selectedTab === "disponiveis"
                ? "bg-white text-black font-semibold"
                : "hover:bg-zinc-700"
                }`}
              onClick={() => setSelectedTab("disponiveis")}
            >
              <FiLayers size={20} /> Disponíveis
            </button>
            <button
              className={`flex items-center gap-3 p-3 w-full rounded-lg text-lg mt-2 transition ${selectedTab === "matricula"
                ? "bg-white text-black font-semibold"
                : "hover:bg-zinc-700"
                }`}
              onClick={() => setSelectedTab("matricula")}
            >
              <FiBook size={20} /> Matricula
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
                  <strong className="text-white">Email:</strong> {aluno.email}
                </p>
                <p className="text-lg text-gray-300">
                  <strong className="text-white">Curso:</strong> {aluno.curso.nome}
                </p>
                <p className="text-lg text-gray-300">
                  <strong className="text-white">Mensalidade:</strong> R${" "}
                  {aluno.matricula.mensalidade}
                </p>
              </div>
            </div>
          ) : selectedTab === "disciplinas" ? (
            <div>
              {/* Disciplinas Matriculadas */}
              <div className="bg-zinc-800 shadow-lg rounded-lg p-6 max-w-3xl mx-auto">
                <h2 className="text-xl font-semibold text-white">
                  Disciplinas Matriculadas
                </h2>
                {aluno.matricula.planoDeEnsino == null ||
                  aluno.matricula.planoDeEnsino.length === 0 ? (
                  <p className="text-lg text-gray-300 mt-4">
                    O aluno não está matriculado em nenhuma disciplina.
                  </p>
                ) : (
                  <ul className="mt-4 space-y-4">
                    {aluno.matricula.planoDeEnsino.map((disciplina) => (
                      <li
                        key={disciplina.id}
                        className="border border-gray-600 rounded-lg p-4 bg-gray-900 shadow-sm"
                      >
                        <div className="flex items-center">
                          <p className="text-lg font-semibold text-white">
                            {disciplina.nome}
                          </p>
                          <OverlayTrigger
                            placement="top"
                            overlay={
                              <Tooltip
                                className="bg-blue-600 text-white rounded-md shadow-lg p-2"
                                id={`tooltip-${disciplina.id}`}
                              >
                                {disciplina.descricao}
                              </Tooltip>
                            }
                          >
                            <FiHelpCircle className="ml-2 text-gray-400 cursor-pointer" />
                          </OverlayTrigger>
                        </div>
                        <p className="text-sm text-gray-300">
                          <strong className="text-white">Período:</strong>{" "}
                          {disciplina.periodo}
                        </p>
                        <p className="text-sm text-gray-300">
                          <strong className="text-white">Campus:</strong>{" "}
                          {disciplina.campus}
                        </p>
                        <p className="text-sm text-gray-300">
                          <strong className="text-white">Professor:</strong>{" "}
                          {disciplina.professor.nome} (
                          {disciplina.professor.nivelEscolar})
                        </p>
                        <p className="text-sm text-gray-300">
                          <strong className="text-white">Ativa:</strong>{" "}
                          {disciplina.isActive}
                        </p>
                      </li>
                    ))}
                  </ul>
                )}
              </div>

              {/* Disciplinas Cursadas */}
              <div className="bg-zinc-800 shadow-lg rounded-lg p-6 max-w-3xl mx-auto mt-6">
                <h2 className="text-xl font-semibold text-white">
                  Disciplinas Passadas
                </h2>
                <ul className="mt-4 space-y-4">
                  {aluno.disciplinasCursadas.map((disciplina) => (
                    <li
                      key={disciplina.id}
                      className="border border-gray-600 rounded-lg p-4 bg-gray-900 shadow-sm"
                    >
                      <p className="text-lg font-semibold text-white">
                        {disciplina.nome}
                      </p>
                      <p className="text-sm text-gray-300">
                        <strong className="text-white">Período:</strong>{" "}
                        {disciplina.periodo}
                      </p>
                      <p className="text-sm text-gray-300">
                        <strong className="text-white">Campus:</strong>{" "}
                        {disciplina.campus}
                      </p>
                      <p className="text-sm text-gray-300">
                        <strong className="text-white">Professor:</strong>{" "}
                        {disciplina.professor.nome} (
                        {disciplina.professor.nivelEscolar})
                      </p>
                      <p className="text-sm text-gray-300">
                          <strong className="text-white">Ativa:</strong> {disciplina.isActive ? "Sim" : "Não"}
                      </p>
                    </li>
                  ))}
                </ul>
              </div>
            </div>
          ) : selectedTab === "disponiveis" ? (
            <div className="bg-zinc-800 shadow-lg rounded-lg p-6 max-w-3xl mx-auto">
              <h2 className="text-xl font-semibold text-white">
                Disciplinas Disponíveis
              </h2>
              {(() => {
                const disciplinasDisponiveis = aluno.curso.disciplinas.filter(
                  (disciplina) =>
                    !aluno.matricula.planoDeEnsino?.some((d) => d.id === disciplina.id) &&
                    !aluno.disciplinasCursadas.some((d) => d.id === disciplina.id)
                );
                if (disciplinasDisponiveis.length === 0) {
                  return (
                    <p className="text-lg text-gray-300 mt-4">
                      Nenhuma disciplina disponível para seu curso.
                    </p>
                  );
                }
                return (
                  <>
                    <ul className="mt-4 space-y-4">
                      {disciplinasDisponiveis.map((disciplina) => (
                        <li
                          key={disciplina.id}
                          className="border border-gray-600 rounded-lg p-4 bg-gray-900 shadow-sm"
                        >
                          <div className="flex items-center">
                            <p className="text-lg font-semibold text-white">
                              {disciplina.nome}
                            </p>
                            <OverlayTrigger
                              placement="top"
                              overlay={
                                <Tooltip
                                  className="bg-blue-600 text-white rounded-md shadow-lg p-2"
                                  id={`tooltip-${disciplina.id}`}
                                >
                                  {disciplina.descricao}
                                </Tooltip>
                              }
                            >
                              <FiHelpCircle className="ml-2 text-gray-400 cursor-pointer" />
                            </OverlayTrigger>
                          </div>
                          <p className="text-sm text-gray-300">
                            <strong className="text-white">Período:</strong>{" "}
                            {disciplina.periodo}
                          </p>
                          <p className="text-sm text-gray-300">
                            <strong className="text-white">Campus:</strong>{" "}
                            {disciplina.campus}
                          </p>
                          <p className="text-sm text-gray-300">
                            <strong className="text-white">Professor:</strong>{" "}
                            {disciplina.professor.nome} (
                            {disciplina.professor.nivelEscolar})
                          </p>
                          <button
                            className={`mt-2 px-4 py-2 rounded-lg text-sm ${selectedDisciplinas.some((d) => d.id === disciplina.id)
                              ? "bg-red-500 text-white"
                              : "bg-green-500 text-white"
                              }`}
                            onClick={() => handleSelectDisciplina(disciplina)}
                          >
                            {selectedDisciplinas.some((d) => d.id === disciplina.id)
                              ? "Cancelar"
                              : "Selecionar disciplina"}
                          </button>
                        </li>
                      ))}
                    </ul>
                    <button
                      className="mt-6 px-6 py-3 bg-blue-500 text-white rounded-lg"
                      onClick={handleConfirmSelection}
                    >
                      Confirmar Seleção
                    </button>
                  </>
                );
              })()}
            </div>
          ) : selectedTab === "matricula" ? (
            <div className="bg-zinc-900 shadow-lg rounded-lg p-6 max-w-3xl mx-auto">
              <h2 className="text-xl font-semibold text-white">Informações da Matrícula</h2>
              <div className="mt-4 space-y-4">
                <p className="text-lg text-gray-300">
                  <strong className="text-white">Número de Matrícula:</strong> {aluno.matricula.numeroDeMatricula || "Não disponível"}
                </p>
                <p className="text-lg text-gray-300">
                  <strong className="text-white">Mensalidade:</strong> R$ {aluno.matricula.mensalidade.toFixed(2)}
                </p>
                <p className="text-lg text-gray-300">
                  <strong className="text-white">Status de pendencias:</strong> {aluno.matricula.paga ? "Pagas" : "Pendente"}
                </p>
                <p className="text-lg text-gray-300">
                  <strong className="text-white">Status:</strong> {aluno.matricula.ativa ? "Ativa" : "Inativa"}
                </p>
                {!aluno.matricula.paga && (
                  <button
                    className="mt-4 px-6 py-3 bg-green-500 text-white rounded-lg hover:bg-green-600 transition"
                    onClick={handlePagarMatricula}
                  >
                    Pagar Matrícula
                  </button>
                )}
              </div>
            </div>
          ) : null}
        </main>
      </div>
    </>
  );
}