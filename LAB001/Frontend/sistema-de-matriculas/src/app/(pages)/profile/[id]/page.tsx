'use server'

import api from '@/api/lib/api'
import Page from '../Profile'
import { Aluno } from '@/@types/aluno.type'

export default async function RedirectToProfilePage({
  params,
}: {
  params: Promise<{ id: string }>
}) {
  const { id } = await params;
  const response = await api.get(`/aluno/get-aluno/${id}`);
  if (response.status == 200) {
    const aluno: Aluno = response.data as Aluno;
    return <Page {...aluno} />
  } else {
    window.location.href = "/" // Ta uma merda, mas acho que da tempo de resolver
  }
}
