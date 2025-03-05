import { Professor } from "./professor.type";

export type Disciplina = {
    id: string
    nome: string;
    isActive: boolean;
    preco: number;
    periodo: string;
    campus: string;
    disciplinasNecessarias: Disciplina[];
    professor: Professor
    quantAlunos: number
    descricao: string
    optativa: boolean
}