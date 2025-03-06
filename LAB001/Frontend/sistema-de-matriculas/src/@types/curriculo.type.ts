import { Aluno } from "./aluno.type"
import { Curso } from "./curso.type"
import { Disciplina } from "./disciplina.type"
import { Professor } from "./professor.type"

export type Curriculo = {
    alunos: Aluno[];
    disciplinas: Disciplina[];
    professores: Professor[];
    cursos: Curso[];
}