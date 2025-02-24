import { Aluno } from "./aluno.type";
import { Professor } from "./professor.type";

export type Disciplina = {
    nome: string;
    isActive: boolean;
    alunos: Aluno[];
    preco: number;
    periodo: string;
    campus: string;
    disciplinasNecessarias: Disciplina[];
    professor: Professor
}