'use client'
import React, { useState, useEffect } from 'react';
import Navbar from '@/components/Navbar';
import {
    createAluno, createCurso, createProfessor,
    createDisciplina, updateAluno, updateCurso, updateDisciplina,
    getCursos, getAlunos, getProfessores, getDisciplinas,
    getCurriculo,
    updateCurriculo
} from '@/api/admin';
import { Curso } from '@/@types/curso.type';
import { Aluno } from '@/@types/aluno.type';
import { Professor } from '@/@types/professor.type';
import { Disciplina } from '@/@types/disciplina.type';
import { Curriculo } from '@/@types/curriculo.type';
import { toast } from "react-toastify"
import { ToastContainer } from 'react-bootstrap';

type MenuItem = 'alunos' | 'curriculos' | 'cursos' | 'disciplinas' | 'professores';

const atualizarCurriculo = async () => {
    const curriculoAtual = {
        alunos: await getAlunos(),
        disciplinas: await getDisciplinas(),
        professores: await getProfessores(),
        cursos: await getCursos(),
        id: '2025.1',
        semestre: '2025.1'
    };
    await updateCurriculo(curriculoAtual);
    toast.success(`Curriculo atualizado com sucesso`, {
        position: "top-right",
        autoClose: 3000,
        theme: "colored",
    })
};

const AdminDashboard: React.FC = () => {
    const [selectedMenu, setSelectedMenu] = useState<MenuItem>('alunos');

    return (
        <>
            <ToastContainer />
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
                                className={`cursor-pointer p-2 rounded hover:bg-zinc-700 ${selectedMenu === 'professores' ? 'bg-zinc-700' : ''}`}
                                onClick={() => setSelectedMenu('professores')}
                            >
                                Professores
                            </li>
                        </ul>
                    </nav>
                </aside>

                {/* Main Content */}
                <main className="flex-1 p-8 overflow-y-auto bg-gray-100">
                    {selectedMenu === 'alunos' && <AlunoManager />}
                    {selectedMenu === 'cursos' && <CursoManager />}
                    {selectedMenu === 'disciplinas' && <DisciplinaManager />}
                    {selectedMenu === 'professores' && <ProfessorForm />}
                    {selectedMenu === 'curriculos' && <CurriculoView />}
                </main>
            </div>
        </>
    );
};

//
// CURSO MANAGER: Handles creating, listing and editing cursos
//
const CursoManager: React.FC = () => {
    type ViewMode = "create" | "list" | "edit";
    const [viewMode, setViewMode] = useState<ViewMode>("create");

    const [nome, setNome] = useState("");
    const [numeroDeCreditos, setNumeroDeCreditos] = useState(0);
    const [cursos, setCursos] = useState<Curso[]>([]);
    const [disciplinas, setDisciplinas] = useState<Disciplina[]>([]);
    const [selectedDisciplinas, setSelectedDisciplinas] = useState<string[]>([]);
    const [editingCurso, setEditingCurso] = useState<Curso>({} as Curso);

    const fetchCursosList = async () => {
        const response = await getCursos();
        setCursos(response);
    };

    const fetchDisciplinasList = async () => {
        const response = await getDisciplinas();
        setDisciplinas(response);
    };

    useEffect(() => {
        if (viewMode === "list") fetchCursosList();
        fetchDisciplinasList();
    }, [viewMode]);

    const handleCreate = async (e: React.FormEvent) => {
        e.preventDefault();
        const novoCurso = {
            nome,
            numeroDeCreditos,
            alunos: [],
            disciplinas: selectedDisciplinas.map(
                (id) => disciplinas.find((d) => d.id === id) as Disciplina
            ),
            id: Math.floor(100000 + Math.random() * 900000).toString(),
        };

        await createCurso(novoCurso);
        setNome("");
        setNumeroDeCreditos(0);
        setSelectedDisciplinas([]);
        fetchCursosList();
        setViewMode("list");
        toast.success(`Curso criado com sucesso`, {
            position: "top-right",
            autoClose: 3000,
            theme: "colored",
        })
    };

    const handleEdit = async (e: React.FormEvent) => {
        e.preventDefault();
        await updateCurso({
            ...editingCurso,
            disciplinas: selectedDisciplinas.map(
                (id) => disciplinas.find((d) => d.id === id) as Disciplina
            ),
        });
        setEditingCurso({} as Curso);
        fetchCursosList();
        setViewMode("list");
        toast.success(`Curso atualizado com sucesso`, {
            position: "top-right",
            autoClose: 3000,
            theme: "colored",
        })
    };

    // Bloco para renderizar a lista de checkboxes para disciplinas
    const renderDisciplinasCheckbox = () => (
        <div>
            <label className="block text-zinc-800 mb-2">Disciplinas</label>
            <div className="flex flex-col">
                {disciplinas.map((disciplina) => (
                    <label key={disciplina.id} className="inline-flex items-center mt-1">
                        <input
                            type="checkbox"
                            value={disciplina.id}
                            checked={selectedDisciplinas.includes(disciplina.id)}
                            onChange={(e) => {
                                const isChecked = e.target.checked;
                                if (isChecked) {
                                    setSelectedDisciplinas([...selectedDisciplinas, disciplina.id]);
                                } else {
                                    setSelectedDisciplinas(
                                        selectedDisciplinas.filter((id) => id !== disciplina.id)
                                    );
                                }
                            }}
                            className="form-checkbox"
                        />
                        <span className="ml-2">{disciplina.nome}</span>
                    </label>
                ))}
            </div>
        </div>
    );

    return (
        <>
            <ToastContainer />
            <div className="max-w-3xl mx-auto bg-white p-6 rounded shadow">
                <div className="flex justify-between mb-4">
                    <button
                        onClick={() => setViewMode("create")}
                        className="px-4 py-2 bg-yellow-500 text-white rounded"
                    >
                        Novo Curso
                    </button>
                    <button
                        onClick={() => setViewMode("list")}
                        className="px-4 py-2 bg-yellow-500 text-white rounded"
                    >
                        Listar Cursos
                    </button>
                </div>

                {viewMode === "create" && (
                    <form onSubmit={handleCreate} className="space-y-4">
                        <h2 className="text-2xl font-bold mb-4 text-zinc-800">Cadastrar Curso</h2>
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
                        {renderDisciplinasCheckbox()}
                        <button
                            type="submit"
                            className="bg-yellow-500 text-white px-4 py-2 rounded hover:bg-yellow-600"
                        >
                            Cadastrar
                        </button>
                    </form>
                )}

                {viewMode === "list" && (
                    <div>
                        <h2 className="text-2xl font-bold mb-4 text-zinc-800">Listagem de Cursos</h2>
                        <table className="min-w-full divide-y divide-gray-200">
                            <thead className="bg-gray-50">
                                <tr>
                                    <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                        Nome
                                    </th>
                                    <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                        Créditos
                                    </th>
                                    <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                        Ações
                                    </th>
                                </tr>
                            </thead>
                            <tbody className="bg-white divide-y divide-gray-200">
                                {cursos.map((curso) => (
                                    <tr key={curso.id}>
                                        <td className="px-6 py-4 whitespace-nowrap">{curso.nome}</td>
                                        <td className="px-6 py-4 whitespace-nowrap">{curso.numeroDeCreditos}</td>
                                        <td className="px-6 py-4 whitespace-nowrap">
                                            <button
                                                onClick={() => {
                                                    setEditingCurso(curso);
                                                    setSelectedDisciplinas(curso.disciplinas.map((d) => d.id));
                                                    setViewMode("edit");
                                                }}
                                                className="px-2 py-1 bg-blue-500 text-white rounded"
                                            >
                                                Editar
                                            </button>
                                        </td>
                                    </tr>
                                ))}
                            </tbody>
                        </table>
                    </div>
                )}

                {viewMode === "edit" && editingCurso && (
                    <form onSubmit={handleEdit} className="space-y-4">
                        <h2 className="text-2xl font-bold mb-4 text-zinc-800">Editar Curso</h2>
                        <div>
                            <label className="block text-zinc-800">Nome</label>
                            <input
                                type="text"
                                value={editingCurso.nome}
                                onChange={(e) =>
                                    setEditingCurso({ ...editingCurso, nome: e.target.value })
                                }
                                className="w-full border border-zinc-300 p-2 rounded"
                            />
                        </div>
                        <div>
                            <label className="block text-zinc-800">Número de Créditos</label>
                            <input
                                type="number"
                                value={editingCurso.numeroDeCreditos}
                                onChange={(e) =>
                                    setEditingCurso({
                                        ...editingCurso,
                                        numeroDeCreditos: Number(e.target.value),
                                    })
                                }
                                className="w-full border border-zinc-300 p-2 rounded"
                            />
                        </div>
                        {renderDisciplinasCheckbox()}
                        <div className="flex space-x-4">
                            <button
                                type="submit"
                                className="bg-yellow-500 text-white px-4 py-2 rounded hover:bg-yellow-600"
                            >
                                Salvar Alterações
                            </button>
                            <button
                                type="button"
                                onClick={() => setViewMode("list")}
                                className="bg-gray-500 text-white px-4 py-2 rounded hover:bg-gray-600"
                            >
                                Cancelar
                            </button>
                        </div>
                    </form>
                )}
            </div>
        </>
    );
};


//
// DISCIPLINA MANAGER: Handles creating, listing and editing disciplinas,
// including allocating a professor and multiple alunos.
//
const DisciplinaManager: React.FC = () => {
    type ViewMode = "create" | "list" | "edit";
    const [viewMode, setViewMode] = useState<ViewMode>("create");

    // Fields for Disciplina
    const [nome, setNome] = useState("");
    const [isActive, setIsActive] = useState(false);
    const [preco, setPreco] = useState(0);
    const [periodo, setPeriodo] = useState("");
    const [campus, setCampus] = useState("");
    const [optativa, setOptativa] = useState(false);
    const [descricao, setDescricao] = useState("");
    const [quantAlunos, setQuantAlunos] = useState(0);

    // Allocation: professor and alunos
    const [professorId, setProfessorId] = useState("");
    const [professores, setProfessores] = useState<Professor[]>([]);

    const [disciplinas, setDisciplinas] = useState<Disciplina[]>([]);
    const [editingDisciplina, setEditingDisciplina] = useState<Disciplina | null>(null);

    // Fetch professors and alunos once on mount
    useEffect(() => {
        const fetchProfessores = async () => {
            const response = await getProfessores();
            setProfessores(response);
        };
        fetchProfessores();
    }, []);

    // Fetch disciplinas when in list mode
    useEffect(() => {
        if (viewMode === "list") {
            const fetchDisciplinas = async () => {
                const response = await getDisciplinas();
                setDisciplinas(response);
            };
            fetchDisciplinas();
        }
    }, [viewMode]);

    const handleCreate = async (e: React.FormEvent) => {
        e.preventDefault();
        const selectedProfessor = professores.find(prof => prof.numeroDePessoa === professorId);

        const novaDisciplina = {
            nome,
            isActive,
            preco,
            periodo,
            campus,
            optativa,
            descricao,
            quantAlunos,
            professorId,
            id: Math.floor(100000 + Math.random() * 900000).toString(),
            disciplinasNecessarias: [],
            professor: selectedProfessor || {} as Professor
        };

        await createDisciplina(novaDisciplina);
        await atualizarCurriculo();
        setNome("");
        setIsActive(false);
        setPreco(0);
        setPeriodo("");
        setCampus("");
        setOptativa(false);
        setDescricao("");
        setQuantAlunos(0);
        setProfessorId("");
        setViewMode("list");
        toast.success(`Disciplina criada com sucesso`, {
            position: "top-right",
            autoClose: 3000,
            theme: "colored",
        })
    };

    const handleEdit = async (e: React.FormEvent) => {
        e.preventDefault();
        if (editingDisciplina) {
            await updateDisciplina(editingDisciplina);
            await atualizarCurriculo();
            setEditingDisciplina(null);
            setViewMode("list");
            toast.success(`Disciplina atualizada com sucesso`, {
                position: "top-right",
                autoClose: 3000,
                theme: "colored",
            })
        }
    };

    return (
        <>
            <ToastContainer />
            <div className="max-w-3xl mx-auto bg-white p-6 rounded shadow">
                <div className="flex justify-between mb-4">
                    <button onClick={() => setViewMode("create")} className="px-4 py-2 bg-yellow-500 text-white rounded">
                        Nova Disciplina
                    </button>
                    <button onClick={() => setViewMode("list")} className="px-4 py-2 bg-yellow-500 text-white rounded">
                        Listar Disciplinas
                    </button>
                </div>

                {viewMode === "create" && (
                    <form onSubmit={handleCreate} className="space-y-4">
                        <h2 className="text-2xl font-bold mb-4 text-zinc-800">Cadastrar Disciplina</h2>
                        <div>
                            <label className="block text-zinc-800">Nome</label>
                            <input type="text" value={nome} onChange={(e) => setNome(e.target.value)} className="w-full border border-zinc-300 p-2 rounded" />
                        </div>
                        <div className="flex items-center">
                            <input type="checkbox" checked={isActive} onChange={(e) => setIsActive(e.target.checked)} className="mr-2" />
                            <label className="text-zinc-800">Ativa</label>
                        </div>
                        <div>
                            <label className="block text-zinc-800">Preço</label>
                            <input type="number" value={preco} onChange={(e) => setPreco(Number(e.target.value))} className="w-full border border-zinc-300 p-2 rounded" />
                        </div>
                        <div>
                            <label className="block text-zinc-800">Período</label>
                            <input type="text" value={periodo} onChange={(e) => setPeriodo(e.target.value)} className="w-full border border-zinc-300 p-2 rounded" placeholder="Ex: Matutino, Vespertino, Noturno" />
                        </div>
                        <div>
                            <label className="block text-zinc-800">Campus</label>
                            <input type="text" value={campus} onChange={(e) => setCampus(e.target.value)} className="w-full border border-zinc-300 p-2 rounded" />
                        </div>
                        <div className="flex items-center">
                            <input type="checkbox" checked={optativa} onChange={(e) => setOptativa(e.target.checked)} className="mr-2" />
                            <label className="text-zinc-800">Optativa</label>
                        </div>
                        <div>
                            <label className="block text-zinc-800">Descrição</label>
                            <textarea value={descricao} onChange={(e) => setDescricao(e.target.value)} className="w-full border border-zinc-300 p-2 rounded" />
                        </div>
                        <div>
                            <label className="block text-zinc-800">Quantidade de Alunos</label>
                            <input type="number" value={quantAlunos} onChange={(e) => setQuantAlunos(Number(e.target.value))} className="w-full border border-zinc-300 p-2 rounded" max={60} />
                        </div>
                        <div>
                            <label className="block text-zinc-800">Professor</label>
                            <select value={professorId} onChange={(e) => setProfessorId(e.target.value)} className="w-full border border-zinc-300 p-2 rounded">
                                <option value="">Selecione um professor</option>
                                {professores.map(prof => (
                                    <option key={prof.numeroDePessoa} value={prof.numeroDePessoa}>{prof.nome}</option>
                                ))}
                            </select>
                        </div>
                        <button type="submit" className="bg-yellow-500 text-white px-4 py-2 rounded hover:bg-yellow-600">
                            Cadastrar
                        </button>
                    </form>
                )}

                {viewMode === "list" && (
                    <div>
                        <h2 className="text-2xl font-bold mb-4 text-zinc-800">Listagem de Disciplinas</h2>
                        <table className="min-w-full divide-y divide-gray-200">
                            <thead className="bg-gray-50">
                                <tr>
                                    <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Nome</th>
                                    <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Professor</th>
                                    <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Alunos Alocados</th>
                                    <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Ações</th>
                                </tr>
                            </thead>
                            <tbody className="bg-white divide-y divide-gray-200">
                                {disciplinas.map(disciplina => (
                                    <tr key={disciplina.id}>
                                        <td className="px-6 py-4 whitespace-nowrap">{disciplina.nome}</td>
                                        <td className="px-6 py-4 whitespace-nowrap">{disciplina.professor?.nome}</td>
                                        <td className="px-6 py-4 whitespace-nowrap">
                                            <button
                                                onClick={() => {
                                                    setEditingDisciplina(disciplina);
                                                    setViewMode("edit");
                                                }}
                                                className="px-2 py-1 bg-blue-500 text-white rounded"
                                            >
                                                Editar
                                            </button>
                                        </td>
                                    </tr>
                                ))}
                            </tbody>
                        </table>
                    </div>
                )}

                {viewMode === "edit" && editingDisciplina && (
                    <form onSubmit={handleEdit} className="space-y-4">
                        <h2 className="text-2xl font-bold mb-4 text-zinc-800">Editar Disciplina</h2>
                        <div>
                            <label className="block text-zinc-800">Nome</label>
                            <input type="text" value={editingDisciplina.nome} onChange={(e) => setEditingDisciplina({ ...editingDisciplina, nome: e.target.value })} className="w-full border border-zinc-300 p-2 rounded" />
                        </div>
                        <div className="flex items-center">
                            <input type="checkbox" checked={editingDisciplina.isActive} onChange={(e) => setEditingDisciplina({ ...editingDisciplina, isActive: e.target.checked })} className="mr-2" />
                            <label className="text-zinc-800">Ativa</label>
                        </div>
                        <div>
                            <label className="block text-zinc-800">Preço</label>
                            <input type="number" value={editingDisciplina.preco} onChange={(e) => setEditingDisciplina({ ...editingDisciplina, preco: Number(e.target.value) })} className="w-full border border-zinc-300 p-2 rounded" />
                        </div>
                        <div>
                            <label className="block text-zinc-800">Período</label>
                            <input type="text" value={editingDisciplina.periodo} onChange={(e) => setEditingDisciplina({ ...editingDisciplina, periodo: e.target.value })} className="w-full border border-zinc-300 p-2 rounded" placeholder="Ex: Matutino, Vespertino, Noturno" />
                        </div>
                        <div>
                            <label className="block text-zinc-800">Campus</label>
                            <input type="text" value={editingDisciplina.campus} onChange={(e) => setEditingDisciplina({ ...editingDisciplina, campus: e.target.value })} className="w-full border border-zinc-300 p-2 rounded" />
                        </div>
                        <div className="flex items-center">
                            <input type="checkbox" checked={editingDisciplina.optativa} onChange={(e) => setEditingDisciplina({ ...editingDisciplina, optativa: e.target.checked })} className="mr-2" />
                            <label className="text-zinc-800">Optativa</label>
                        </div>
                        <div>
                            <label className="block text-zinc-800">Descrição</label>
                            <textarea value={editingDisciplina.descricao} onChange={(e) => setEditingDisciplina({ ...editingDisciplina, descricao: e.target.value })} className="w-full border border-zinc-300 p-2 rounded" />
                        </div>
                        <div>
                            <label className="block text-zinc-800">Quantidade de Alunos</label>
                            <input type="number" value={editingDisciplina.quantAlunos} onChange={(e) => setEditingDisciplina({ ...editingDisciplina, quantAlunos: Number(e.target.value) })} className="w-full border border-zinc-300 p-2 rounded" max={60} />
                        </div>
                        <div>
                            <label className="block text-zinc-800">Professor</label>
                            <select value={editingDisciplina.professor.numeroDePessoa} onChange={(e) => setEditingDisciplina({ ...editingDisciplina, id: e.target.value })} className="w-full border border-zinc-300 p-2 rounded">
                                <option value="">Selecione um professor</option>
                                {professores.map(prof => (
                                    <option key={prof.numeroDePessoa} value={prof.numeroDePessoa}>{prof.nome}</option>
                                ))}
                            </select>
                        </div>
                        <div className="flex space-x-4">
                            <button type="submit" className="bg-yellow-500 text-white px-4 py-2 rounded hover:bg-yellow-600">
                                Salvar Alterações
                            </button>
                            <button type="button" onClick={() => setViewMode("list")} className="bg-gray-500 text-white px-4 py-2 rounded hover:bg-gray-600">
                                Cancelar
                            </button>
                        </div>
                    </form>
                )}
            </div>
        </>
    );
};

//
// COMPONENTE: Visualização do Currículo Atual
//
const CurriculoView: React.FC = () => {
    const [curriculo, setCurriculo] = useState<Curriculo>({} as Curriculo);

    useEffect(() => {
        const fetchCurriculo = async () => {
            const response = await getCurriculo();
            setCurriculo(response);
        };
        fetchCurriculo();
    }, []);

    return (
        <>
            <ToastContainer />
            <div className="max-w-3xl mx-auto bg-white p-6 rounded shadow">
                <h2 className="text-2xl font-bold mb-4 text-zinc-800">Currículo Atual</h2>
                {curriculo ? (
                    <div>
                        <h3 className="text-xl font-bold mb-2 text-zinc-800">Semestre</h3>
                        <ul className="mb-4">
                            {curriculo.semestre}
                        </ul>
                        <h3 className="text-xl font-bold mb-2 text-zinc-800">Alunos</h3>
                        <ul className="mb-4">
                            {curriculo.alunos?.map(aluno => (
                                <li key={aluno.numeroDePessoa}>{aluno.nome}</li>
                            ))}
                        </ul>
                        <h3 className="text-xl font-bold mb-2 text-zinc-800">Professores</h3>
                        <ul className="mb-4">
                            {curriculo.professores?.map(professor => (
                                <li key={professor.numeroDePessoa}>{professor.nome}</li>
                            ))}
                        </ul>
                        <h3 className="text-xl font-bold mb-2 text-zinc-800">Disciplinas</h3>
                        <ul className="mb-4">
                            {curriculo.disciplinas?.map(disciplina => (
                                <li key={disciplina.id}>{disciplina.nome}</li>
                            ))}
                        </ul>
                        <h3 className="text-xl font-bold mb-2 text-zinc-800">Cursos</h3>
                        <ul>
                            {curriculo.cursos?.map(curso => (
                                <li key={curso.id}>{curso.nome}</li>
                            ))}
                        </ul>
                    </div>
                ) : (
                    <p>Carregando...</p>
                )}
            </div>
        </>
    );
};

//
// EXEMPLO: Aluno Manager com chamada ao updateCurriculo
//
const AlunoManager: React.FC = () => {
    type ViewMode = "create" | "list" | "edit";
    const [viewMode, setViewMode] = useState<ViewMode>("create");

    const [nome, setNome] = useState("");
    const [senha, setSenha] = useState("");
    const [email, setEmail] = useState("");
    const [cursoId, setCursoId] = useState("");
    const [cursos, setCursos] = useState<Curso[]>([]);
    const [alunos, setAlunos] = useState<Aluno[]>([]);
    const [editingAluno, setEditingAluno] = useState<Aluno>({} as Aluno);
    const [disciplinasAprovadas, setDisciplinasAprovadas] = useState<string[]>([]);


    useEffect(() => {
        const fetchCursos = async () => {
            const response = await getCursos();
            setCursos(response);
        };
        fetchCursos();
    }, []);

    const fetchAlunos = async () => {
        const response = await getAlunos();
        setAlunos(response);
    };

    useEffect(() => {
        if (viewMode === "list") fetchAlunos();
    }, [viewMode]);


    const handleCreate = async (e: React.FormEvent) => {
        e.preventDefault();
        const selectedCurso = cursos.find(curso => curso.id === cursoId);
        const numeroDeMatricula = Math.floor(100000 + Math.random() * 900000).toString();
        if (selectedCurso) {
            const aluno = {
                nome,
                senha,
                email,
                curso: selectedCurso,
                matricula: {
                    numeroDeMatricula: numeroDeMatricula,
                    ativa: false,
                    planoDeEnsino: [],
                    mensalidade: 0,
                    paga: false
                },
                MatriculaId: numeroDeMatricula,
                disciplinasCursadas: []
            };
            await createAluno(aluno);
            await atualizarCurriculo();

            setNome("");
            setSenha("");
            setEmail("");
            setCursoId("");
            fetchAlunos();
            setViewMode("list");
            toast.success(`Aluno criado com sucesso`, {
                position: "top-right",
                autoClose: 3000,
                theme: "colored",
            })
        } else {
            console.error("Curso não selecionado");
        }
    };

    const handleEdit = async (e: React.FormEvent) => {
        e.preventDefault();
        if (editingAluno.matricula && Array.isArray(editingAluno.matricula.planoDeEnsino)) {
            const aprovados = editingAluno.matricula.planoDeEnsino.filter((disc: Disciplina) =>
                disciplinasAprovadas.includes(disc.id)
            );
            const novoPlano = editingAluno.matricula.planoDeEnsino.filter((disc: Disciplina) =>
                !disciplinasAprovadas.includes(disc.id)
            );
            editingAluno.matricula.planoDeEnsino = novoPlano;
            editingAluno.disciplinasCursadas = [
                ...editingAluno.disciplinasCursadas,
                ...aprovados
            ];
        }
        await updateAluno(editingAluno);
        await atualizarCurriculo();
        setEditingAluno({} as Aluno);
        fetchAlunos();
        setViewMode("list");
        toast.success(`Aluno atualizado com sucesso`, {
            position: "top-right",
            autoClose: 3000,
            theme: "colored",
        });
        // Limpe a seleção
        setDisciplinasAprovadas([]);
    };

    return (
        <>
            <ToastContainer />
            <div className="max-w-3xl mx-auto bg-white p-6 rounded shadow">
                <div className="flex justify-between mb-4">
                    <button onClick={() => setViewMode("create")} className="px-4 py-2 bg-yellow-500 text-white rounded">
                        Novo Aluno
                    </button>
                    <button onClick={() => setViewMode("list")} className="px-4 py-2 bg-yellow-500 text-white rounded">
                        Listar Alunos
                    </button>
                </div>

                {viewMode === "create" && (
                    <form onSubmit={handleCreate} className="space-y-4">
                        <h2 className="text-2xl font-bold mb-4 text-zinc-800">Cadastrar Aluno</h2>
                        <div>
                            <label className="block text-zinc-800">Nome</label>
                            <input type="text" value={nome} onChange={(e) => setNome(e.target.value)} className="w-full border border-zinc-300 p-2 rounded" />
                        </div>
                        <div>
                            <label className="block text-zinc-800">Senha</label>
                            <input type="password" value={senha} onChange={(e) => setSenha(e.target.value)} className="w-full border border-zinc-300 p-2 rounded" />
                        </div>
                        <div>
                            <label className="block text-zinc-800">Email</label>
                            <input type="email" value={email} onChange={(e) => setEmail(e.target.value)} className="w-full border border-zinc-300 p-2 rounded" />
                        </div>
                        <div>
                            <label className="block text-zinc-800">Curso</label>
                            <select value={cursoId} onChange={(e) => setCursoId(e.target.value)} className="w-full border border-zinc-300 p-2 rounded">
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
                )}

                {viewMode === "list" && (
                    <div>
                        <h2 className="text-2xl font-bold mb-4 text-zinc-800">Listagem de Alunos</h2>
                        <table className="min-w-full divide-y divide-gray-200">
                            <thead className="bg-gray-50">
                                <tr>
                                    <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Nome</th>
                                    <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Matricula</th>
                                    <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Email</th>
                                    <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Curso</th>
                                    <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Ações</th>
                                </tr>
                            </thead>
                            <tbody className="bg-white divide-y divide-gray-200">
                                {alunos.map(aluno => (
                                    <tr key={aluno.numeroDePessoa}>
                                        <td className="px-6 py-4 whitespace-nowrap">{aluno.nome}</td>
                                        <td className="px-6 py-4 whitespace-nowrap">{aluno.matricula.numeroDeMatricula}</td>
                                        <td className="px-6 py-4 whitespace-nowrap">{aluno.email}</td>
                                        <td className="px-6 py-4 whitespace-nowrap">{aluno.curso.nome}</td>
                                        <td className="px-6 py-4 whitespace-nowrap">
                                        <button
                                                onClick={() => {
                                                    setEditingAluno({
                                                        ...aluno,
                                                        matricula: {
                                                            ...aluno.matricula,
                                                            planoDeEnsino: aluno.matricula?.planoDeEnsino || []
                                                        }
                                                    });

                                                    setViewMode("edit");
                                                }}
                                                className="px-2 py-1 bg-blue-500 text-white rounded"
                                            >
                                                Editar
                                            </button>
                                            </td>
                                    </tr>
                                ))}
                            </tbody>
                        </table>
                    </div>
                )}

                {viewMode === "edit" && editingAluno && (
                    <form onSubmit={handleEdit} className="space-y-4">
                        <h2 className="text-2xl font-bold mb-4 text-zinc-800">Editar Aluno</h2>
                        <div>
                            <label className="block text-zinc-800">Nome</label>
                            <input
                                type="text"
                                value={editingAluno.nome}
                                onChange={(e) => setEditingAluno({ ...editingAluno, nome: e.target.value })}
                                className="w-full border border-zinc-300 p-2 rounded"
                            />
                        </div>
                        <div>
                            <label className="block text-zinc-800">Email</label>
                            <input
                                type="email"
                                value={editingAluno.email}
                                onChange={(e) => setEditingAluno({ ...editingAluno, email: e.target.value })}
                                className="w-full border border-zinc-300 p-2 rounded"
                            />
                        </div>
                        <div>
                            <label className="block text-zinc-800">Matrícula Paga</label>
                            <input
                                type="checkbox"
                                checked={editingAluno.matricula?.paga}
                                onChange={(e) =>
                                    setEditingAluno({
                                        ...editingAluno,
                                        matricula: {
                                            ...editingAluno.matricula,
                                            paga: e.target.checked,
                                        },
                                    })
                                }
                                className="form-checkbox mt-1"
                            />
                        </div>
                        {editingAluno.matricula &&
                            Array.isArray(editingAluno.matricula.planoDeEnsino) && (
                                <fieldset className="border p-4 rounded">
                                    <legend className="font-bold text-zinc-800">Marcar disciplinas aprovadas</legend>
                                    <div className="flex flex-col">
                                        {editingAluno.matricula.planoDeEnsino.map((disc: Disciplina) => (
                                            <label key={disc.id} className="inline-flex items-center mt-1">
                                                <input
                                                    type="checkbox"
                                                    value={disc.id}
                                                    checked={disciplinasAprovadas.includes(disc.id)}
                                                    onChange={(e) => {
                                                        const isChecked = e.target.checked;
                                                        if (isChecked) {
                                                            setDisciplinasAprovadas([...disciplinasAprovadas, disc.id]);
                                                        } else {
                                                            setDisciplinasAprovadas(
                                                                disciplinasAprovadas.filter((id) => id !== disc.id)
                                                            );
                                                        }
                                                    }}
                                                    className="form-checkbox"
                                                />
                                                <span className="ml-2">{disc.nome}</span>
                                            </label>
                                        ))}
                                    </div>
                                </fieldset>
                            )}
                        <div className="flex space-x-4">
                            <button type="submit" className="bg-yellow-500 text-white px-4 py-2 rounded hover:bg-yellow-600">
                                Salvar Alterações
                            </button>
                            <button type="button" onClick={() => setViewMode("list")} className="bg-gray-500 text-white px-4 py-2 rounded hover:bg-gray-600">
                                Cancelar
                            </button>
                        </div>
                    </form>
                )}
            </div>
        </>
    );
};

//
// PROFESSOR FORM permanece sem alterações, pois é apenas um formulário de cadastro
//
const ProfessorForm: React.FC = () => {
    const [nome, setNome] = useState("");
    const [senha, setSenha] = useState("");
    const [email, setEmail] = useState("");
    const [nivelEscolar, setNivelEscolar] = useState("");

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        createProfessor({
            nome, senha, nivelEscolar, disciplinas: [], email
        });
        toast.success(`Professor criado com sucesso`, {
            position: "top-right",
            autoClose: 3000,
            theme: "colored",
        })
    };

    return (
        <>
            <ToastContainer />
            <div className="max-w-lg mx-auto bg-white p-6 rounded shadow">
                <h2 className="text-2xl font-bold mb-4 text-zinc-800">Cadastrar Professor</h2>
                <form onSubmit={handleSubmit} className="space-y-4">
                    <div>
                        <label className="block text-zinc-800">Nome</label>
                        <input type="text" value={nome} onChange={(e) => setNome(e.target.value)} className="w-full border border-zinc-300 p-2 rounded" />
                    </div>
                    <div>
                        <label className="block text-zinc-800">Email</label>
                        <input type="email" value={email} onChange={(e) => setEmail(e.target.value)} className="w-full border border-zinc-300 p-2 rounded" />
                    </div>
                    <div>
                        <label className="block text-zinc-800">Senha</label>
                        <input type="password" value={senha} onChange={(e) => setSenha(e.target.value)} className="w-full border border-zinc-300 p-2 rounded" />
                    </div>
                    <div>
                        <label className="block text-zinc-800">Nível Escolar</label>
                        <input type="text" value={nivelEscolar} onChange={(e) => setNivelEscolar(e.target.value)} className="w-full border border-zinc-300 p-2 rounded" placeholder="Ex: Mestrado, Doutorado" />
                    </div>
                    <button type="submit" className="bg-yellow-500 text-white px-4 py-2 rounded hover:bg-yellow-600">
                        Cadastrar
                    </button>
                </form>
            </div>
        </>
    );
};

export default AdminDashboard;
