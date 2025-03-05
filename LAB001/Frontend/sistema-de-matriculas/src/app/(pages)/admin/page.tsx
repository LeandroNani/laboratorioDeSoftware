'use client'
import { createAluno, createCurso } from '@/api/admin';
import React, { useState } from 'react';
import { useEffect } from 'react';
import { getCursos } from '@/api/admin';
import { Curso } from '@/@types/curso.type';
import Navbar from '@/components/Navbar';

type MenuItem = 'alunos' | 'curriculos' | 'cursos' | 'disciplinas' | 'matriculas' | 'professores';

const AdminDashboard: React.FC = () => {
    const [selectedMenu, setSelectedMenu] = useState<MenuItem>('alunos');

    return (
        <>
            <Navbar />
                <div className="flex h-screen">
                {/* Sidebar */}
                <aside className="w-64 bg-zinc-800 text-white p-4">
                    <h1 className="text-2xl font-bold text-yellow-500 mb-6">Painel Administrativo</h1>
                    <nav>
                        <ul className="space-y-2">
                            <li
                                className={`cursor-pointer p-2 rounded hover:bg-zinc-700 ${selectedMenu === 'alunos' ? 'bg-zinc-700' : ''}`}
                                onClick={() => setSelectedMenu('alunos')}
                            >
                                Alunos
                            </li>
                            <li
                                className={`cursor-pointer p-2 rounded hover:bg-zinc-700 ${selectedMenu === 'curriculos' ? 'bg-zinc-700' : ''}`}
                                onClick={() => setSelectedMenu('curriculos')}
                            >
                                Currículos
                            </li>
                            <li
                                className={`cursor-pointer p-2 rounded hover:bg-zinc-700 ${selectedMenu === 'cursos' ? 'bg-zinc-700' : ''}`}
                                onClick={() => setSelectedMenu('cursos')}
                            >
                                Cursos
                            </li>
                            <li
                                className={`cursor-pointer p-2 rounded hover:bg-zinc-700 ${selectedMenu === 'disciplinas' ? 'bg-zinc-700' : ''}`}
                                onClick={() => setSelectedMenu('disciplinas')}
                            >
                                Disciplinas
                            </li>
                            <li
                                className={`cursor-pointer p-2 rounded hover:bg-zinc-700 ${selectedMenu === 'matriculas' ? 'bg-zinc-700' : ''}`}
                                onClick={() => setSelectedMenu('matriculas')}
                            >
                                Matrículas
                            </li>
                            <li
                                className={`cursor-pointer p-2 rounded hover:bg-zinc-700 ${selectedMenu === 'professores' ? 'bg-zinc-700' : ''}`}
                                onClick={() => setSelectedMenu('professores')}
                            >
                                Professores
                            </li>
                        </ul>
                    </nav>
                </aside>

                {/* Área de conteúdo */}
                <main className="flex-1 p-8 overflow-y-auto bg-gray-100">
                    {selectedMenu === 'alunos' && <AlunoForm />}
                    {selectedMenu === 'curriculos' && <CurriculoForm />}
                    {selectedMenu === 'cursos' && <CursoForm />}
                    {selectedMenu === 'disciplinas' && <DisciplinaForm />}
                    {selectedMenu === 'matriculas' && <MatriculaForm />}
                    {selectedMenu === 'professores' && <ProfessorForm />}
                </main>
            </div>
        </>
    );
};

const AlunoForm: React.FC = () => {
    const [nome, setNome] = useState("");
    const [senha, setSenha] = useState("");
    const [email, setEmail] = useState("");
    const [cursoId, setCursoId] = useState("");
    const [cursos, setCursos] = useState<Curso[]>([]);

    useEffect(() => {
        const fetchCursos = async () => {
            const response = await getCursos();
            setCursos(response);
        };

        fetchCursos();
    }, []);

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        const selectedCurso = cursos.find(curso => curso.id === cursoId);
        if (selectedCurso) {
            const aluno = {
                nome, senha, email, curso: selectedCurso,
                matricula: {
                    numeroDeMatricula: "1234",
                    ativa: false,
                    planoDeEnsino: [],
                    mensalidade: 0
                },
                matriculaId: "1234",
                disciplinasCursadas: []
            }
            createAluno(aluno);
        } else {
            console.error("Curso não selecionado");
        }
    };

    return (
        <div className="max-w-lg mx-auto bg-white p-6 rounded shadow">
            <h2 className="text-2xl font-bold mb-4 text-zinc-800">Cadastrar Aluno</h2>
            <form onSubmit={handleSubmit} className="space-y-4">
                <div>
                    <label className="block text-zinc-800">Nome</label>
                    <input
                        type="text"
                        value={nome}
                        onChange={(e) => setNome(e.target.value)}
                        className="w-full border border-zinc-300 p-2 rounded"
                    />
                </div>
                <div>
                    <label className="block text-zinc-800">Senha</label>
                    <input
                        type="password"
                        value={senha}
                        onChange={(e) => setSenha(e.target.value)}
                        className="w-full border border-zinc-300 p-2 rounded"
                    />
                </div>
                <div>
                    <label className="block text-zinc-800">Email</label>
                    <input
                        type="email"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                        className="w-full border border-zinc-300 p-2 rounded"
                    />
                </div>
                <div>
                    <label className="block text-zinc-800">Curso</label>
                    <select
                        value={cursoId}
                        onChange={(e) => setCursoId(e.target.value)}
                        className="w-full border border-zinc-300 p-2 rounded"
                    >
                        <option value="">Selecione um curso</option>
                        {cursos.map(curso => (
                            <option key={curso.id} value={curso.id}>
                                {curso.nome}
                            </option>
                        ))}
                    </select>
                </div>
                <button type="submit" className="bg-yellow-500 text-white px-4 py-2 rounded hover:bg-yellow-600">
                    Cadastrar
                </button>
            </form>
        </div>
    );
};

const CurriculoForm: React.FC = () => {
    const [semestre, setSemestre] = useState("");

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        // Integre com a API para cadastrar o currículo
        console.log({ semestre });
    };

    return (
        <div className="max-w-lg mx-auto bg-white p-6 rounded shadow">
            <h2 className="text-2xl font-bold mb-4 text-zinc-800">Cadastrar Currículo</h2>
            <form onSubmit={handleSubmit} className="space-y-4">
                <div>
                    <label className="block text-zinc-800">Semestre</label>
                    <input
                        type="text"
                        value={semestre}
                        onChange={(e) => setSemestre(e.target.value)}
                        className="w-full border border-zinc-300 p-2 rounded"
                        placeholder="Ex: 2023.1"
                    />
                </div>
                <button type="submit" className="bg-yellow-500 text-white px-4 py-2 rounded hover:bg-yellow-600">
                    Cadastrar
                </button>
            </form>
        </div>
    );
};

const CursoForm: React.FC = () => {
    const [nome, setNome] = useState("");
    const [numeroDeCreditos, setNumeroDeCreditos] = useState(0);

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        createCurso({ nome, numeroDeCreditos, alunos: [], disciplinas: [], id: Math.floor(100000 + Math.random() * 900000).toString() });
        console.log({ nome, numeroDeCreditos });
    };

    return (
        <div className="max-w-lg mx-auto bg-white p-6 rounded shadow">
            <h2 className="text-2xl font-bold mb-4 text-zinc-800">Cadastrar Curso</h2>
            <form onSubmit={handleSubmit} className="space-y-4">
                <div>
                    <label className="block text-zinc-800">Nome</label>
                    <input
                        type="text"
                        value={nome}
                        onChange={(e) => setNome(e.target.value)}
                        className="w-full border border-zinc-300 p-2 rounded"
                    />
                </div>
                <div>
                    <label className="block text-zinc-800">Número de Créditos</label>
                    <input
                        type="number"
                        value={numeroDeCreditos}
                        onChange={(e) => setNumeroDeCreditos(Number(e.target.value))}
                        className="w-full border border-zinc-300 p-2 rounded"
                    />
                </div>
                <button type="submit" className="bg-yellow-500 text-white px-4 py-2 rounded hover:bg-yellow-600">
                    Cadastrar
                </button>
            </form>
        </div>
    );
};

const DisciplinaForm: React.FC = () => {
    const [nome, setNome] = useState("");
    const [isActive, setIsActive] = useState(false);
    const [professorId, setProfessorId] = useState("");
    const [preco, setPreco] = useState(0);
    const [periodo, setPeriodo] = useState("");
    const [campus, setCampus] = useState("");
    const [optativa, setOptativa] = useState(false);
    const [descricao, setDescricao] = useState("");
    const [quantAlunos, setQuantAlunos] = useState(0);

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        // Integre com a API para cadastrar a disciplina
        console.log({ nome, isActive, professorId, preco, periodo, campus, optativa, descricao, quantAlunos });
    };

    return (
        <div className="max-w-lg mx-auto bg-white p-6 rounded shadow">
            <h2 className="text-2xl font-bold mb-4 text-zinc-800">Cadastrar Disciplina</h2>
            <form onSubmit={handleSubmit} className="space-y-4">
                <div>
                    <label className="block text-zinc-800">Nome</label>
                    <input
                        type="text"
                        value={nome}
                        onChange={(e) => setNome(e.target.value)}
                        className="w-full border border-zinc-300 p-2 rounded"
                    />
                </div>
                <div className="flex items-center">
                    <input
                        type="checkbox"
                        checked={isActive}
                        onChange={(e) => setIsActive(e.target.checked)}
                        className="mr-2"
                    />
                    <label className="text-zinc-800">Ativo</label>
                </div>
                <div>
                    <label className="block text-zinc-800">Professor ID</label>
                    <input
                        type="text"
                        value={professorId}
                        onChange={(e) => setProfessorId(e.target.value)}
                        className="w-full border border-zinc-300 p-2 rounded"
                        placeholder="Informe o ID do professor"
                    />
                </div>
                <div>
                    <label className="block text-zinc-800">Preço</label>
                    <input
                        type="number"
                        value={preco}
                        onChange={(e) => setPreco(Number(e.target.value))}
                        className="w-full border border-zinc-300 p-2 rounded"
                    />
                </div>
                <div>
                    <label className="block text-zinc-800">Período</label>
                    <input
                        type="text"
                        value={periodo}
                        onChange={(e) => setPeriodo(e.target.value)}
                        className="w-full border border-zinc-300 p-2 rounded"
                        placeholder="Ex: Matutino, Vespertino, Noturno"
                    />
                </div>
                <div>
                    <label className="block text-zinc-800">Campus</label>
                    <input
                        type="text"
                        value={campus}
                        onChange={(e) => setCampus(e.target.value)}
                        className="w-full border border-zinc-300 p-2 rounded"
                    />
                </div>
                <div className="flex items-center">
                    <input
                        type="checkbox"
                        checked={optativa}
                        onChange={(e) => setOptativa(e.target.checked)}
                        className="mr-2"
                    />
                    <label className="text-zinc-800">Optativa</label>
                </div>
                <div>
                    <label className="block text-zinc-800">Descrição</label>
                    <textarea
                        value={descricao}
                        onChange={(e) => setDescricao(e.target.value)}
                        className="w-full border border-zinc-300 p-2 rounded"
                    />
                </div>
                <div>
                    <label className="block text-zinc-800">Quantidade de Alunos</label>
                    <input
                        type="number"
                        value={quantAlunos}
                        onChange={(e) => setQuantAlunos(Number(e.target.value))}
                        className="w-full border border-zinc-300 p-2 rounded"
                        max={60}
                    />
                </div>
                <button type="submit" className="bg-yellow-500 text-white px-4 py-2 rounded hover:bg-yellow-600">
                    Cadastrar
                </button>
            </form>
        </div>
    );
};

const MatriculaForm: React.FC = () => {
    const [ativa, setAtiva] = useState(false);
    const [mensalidade, setMensalidade] = useState(0);

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        // Integre com a API para cadastrar a matrícula
        console.log({ ativa, mensalidade });
    };

    return (
        <div className="max-w-lg mx-auto bg-white p-6 rounded shadow">
            <h2 className="text-2xl font-bold mb-4 text-zinc-800">Cadastrar Matrícula</h2>
            <form onSubmit={handleSubmit} className="space-y-4">
                <div className="flex items-center">
                    <input
                        type="checkbox"
                        checked={ativa}
                        onChange={(e) => setAtiva(e.target.checked)}
                        className="mr-2"
                    />
                    <label className="text-zinc-800">Ativa</label>
                </div>
                <div>
                    <label className="block text-zinc-800">Mensalidade</label>
                    <input
                        type="number"
                        value={mensalidade}
                        onChange={(e) => setMensalidade(Number(e.target.value))}
                        className="w-full border border-zinc-300 p-2 rounded"
                    />
                </div>
                <button type="submit" className="bg-yellow-500 text-white px-4 py-2 rounded hover:bg-yellow-600">
                    Cadastrar
                </button>
            </form>
        </div>
    );
};

const ProfessorForm: React.FC = () => {
    const [nome, setNome] = useState("");
    const [senha, setSenha] = useState("");
    const [nivelEscolar, setNivelEscolar] = useState("");

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        // Integre com a API para cadastrar o professor
        console.log({ nome, senha, nivelEscolar });
    };

    return (
        <div className="max-w-lg mx-auto bg-white p-6 rounded shadow">
            <h2 className="text-2xl font-bold mb-4 text-zinc-800">Cadastrar Professor</h2>
            <form onSubmit={handleSubmit} className="space-y-4">
                <div>
                    <label className="block text-zinc-800">Nome</label>
                    <input
                        type="text"
                        value={nome}
                        onChange={(e) => setNome(e.target.value)}
                        className="w-full border border-zinc-300 p-2 rounded"
                    />
                </div>
                <div>
                    <label className="block text-zinc-800">Senha</label>
                    <input
                        type="password"
                        value={senha}
                        onChange={(e) => setSenha(e.target.value)}
                        className="w-full border border-zinc-300 p-2 rounded"
                    />
                </div>
                <div>
                    <label className="block text-zinc-800">Nível Escolar</label>
                    <input
                        type="text"
                        value={nivelEscolar}
                        onChange={(e) => setNivelEscolar(e.target.value)}
                        className="w-full border border-zinc-300 p-2 rounded"
                        placeholder="Ex: Mestrado, Doutorado"
                    />
                </div>
                <button type="submit" className="bg-yellow-500 text-white px-4 py-2 rounded hover:bg-yellow-600">
                    Cadastrar
                </button>
            </form>
        </div>
    );
};

export default AdminDashboard;
