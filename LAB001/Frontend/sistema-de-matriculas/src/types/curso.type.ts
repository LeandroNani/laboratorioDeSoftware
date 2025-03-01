import { Aluno } from "./aluno.type";
import { Disciplina } from "./disciplina.type";

export type Curso = {
    id: string;
    nome: string;
    numeroDeCreditos: number;
    alunos: Aluno[];
    disciplinas: Disciplina[];
}