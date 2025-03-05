import { Curso } from "@/@types/curso.type";
import { Disciplina } from "@/@types/disciplina.type";
import { Professor } from "@/@types/professor.type";
import { Aluno } from "@/@types/aluno.type";
import { Curriculo } from "@/@types/curriculo.type";
import api from "./lib/api";

export async function createCurso(curso: Curso) {
    const response = await api.post("/curso/novo-curso", curso)
    return response
}

export async function createDisciplina(disciplina: Disciplina) {
    console.log(disciplina)
    const response = await api.post("/disciplina/nova-disciplina", disciplina);
    return response;
}

export async function createProfessor(professor: Professor) {
    console.log(professor)
    const response = await api.post("/professor/novo-professor", professor);
    return response;
}

export function createAluno(aluno: Aluno) {
    console.log(aluno)
    const response = api.post("/aluno/novo-aluno", aluno);
    return response;
}

export async function createCurriculo(curriculo: Curriculo) {
    console.log(curriculo)
    const response = await api.post("/curriculo/novo-curriculo", curriculo);
    return response;
}

export async function getCurriculo() {
    const response = await api.get("")
    return response.data as Curriculo
}

export async function getDisciplinas() {
    const response = await api.get("/disciplina/listar");
    return response.data as Disciplina[];
}

export async function getCursos() {
    const response = await api.get("/curso/listar");
    return response.data as Curso[]
}

export async function getProfessores() {
    const response = await api.get("/professor/listar");
    return response.data as Professor[];
}

export async function getAlunos() {
    const response = await api.get("/aluno/listar");
    return response.data as Aluno[]
}
