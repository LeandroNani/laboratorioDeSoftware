import { Aluno } from "./aluno.type";
import { Professor } from "./professor.type";

export type Disciplina = {
    id: string
    nome: string;
    isActive: boolean;
    alunos: Aluno[];
    preco: number;
    periodo: string;
    campus: string;
    disciplinasNecessarias: Disciplina[];
    professor: Professor
}