import { Curso } from "./curso.type";
import { Disciplina } from "./disciplina.type";
import { Pessoa } from "./pessoa.type";

export type Aluno = Pessoa & {
    matricula: string;
    mensalidade: number;
    curso?: Curso;
    disciplinasCursadas: Disciplina[];
}