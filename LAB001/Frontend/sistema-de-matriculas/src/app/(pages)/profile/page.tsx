'use client'
// import { GetServerSideProps } from 'next';
import { Aluno } from '@/types/aluno.type';

interface PerfilProps {
  aluno: Aluno;
}

const Perfil: React.FC<PerfilProps> = ({ aluno }) => {
  return (
    <div>
      <h1>Perfil do Aluno</h1>
      <p><strong>Matrícula:</strong> {aluno.matricula}</p>
      <p><strong>Email:</strong> {aluno.email}</p>
      <p><strong>Mensalidade:</strong> R$ {aluno.mensalidade}</p>

      {aluno.curso && (
        <>
          <h2>Curso</h2>
          <p><strong>Nome:</strong> {aluno.curso.nome}</p>
          <p><strong>Número de Créditos:</strong> {aluno.curso.numeroDeCreditos}</p>
        </>
      )}

      {aluno.planoDeEnsino && (
        <>
          <h2>Disciplinas Atuais</h2>
          <ul>
            {aluno.planoDeEnsino.map((disciplina) => (
              <li key={disciplina.id}>
                {disciplina.nome} - {disciplina.periodo} - {disciplina.campus}
              </li>
            ))}
          </ul>
        </>
      )}
    </div>
  );
};

const ProfilePage: React.FC = () => {
  const aluno: Aluno = {
    numeroDePessoa: 456,
    nome: 'Aluno Exemplo',
    senha: 'senha123',
    matricula: '20250001',
    email: 'aluno@example.com',
    mensalidade: 1500,
    curso: {
      id: '1',
      nome: 'Engenharia de Software',
      numeroDeCreditos: 180,
      alunos: [],
      disciplinas: [],
    },
    planoDeEnsino: [
      {
        id: '1',
        nome: 'Algoritmos Avançados',
        isActive: true,
        preco: 500,
        periodo: '2025.1',
        campus: 'Campus Principal',
        alunos: [],
        disciplinasNecessarias: [],
        professor: {numeroDePessoa: 123, nome: 'Professor Example', senha: 'password', disciplinas: [], nivelEscolar: "Doutor"},
      },
      {
        id: '2',
        nome: 'Sistemas Distribuídos',
        isActive: true,
        preco: 600,
        periodo: '2025.1',
        campus: 'Campus Principal',
        alunos: [],
        disciplinasNecessarias: [],
        professor: {numeroDePessoa: 123, nome: 'Professor Example', senha: 'password', disciplinas: [], nivelEscolar: "Doutor"},
      },
    ],
    disciplinasCursadas: [
      {
        id: '3',
        nome: 'Introdução à Programação',
        isActive: true,
        preco: 400,
        periodo: '2024.2',
        campus: 'Campus Principal',
        alunos: [],
        disciplinasNecessarias: [],
        professor: {numeroDePessoa: 124, nome: 'Professor Example 2', senha: 'password', disciplinas: [], nivelEscolar: "Mestre"},
      },
    ],
  };

  return (
    <Perfil aluno={aluno} />
  );
};

export default ProfilePage;
