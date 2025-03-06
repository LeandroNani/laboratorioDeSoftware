'use server'

import api from '@/api/lib/api'
import Page, { dados } from '../Professor'

export default async function RedirectToProfilePage({
  params,
}: {
  params: Promise<{ id: string }>
}) {
  const { id } = await params;
  const response = await api.get(`/professor/get-professor/${id}`);
  if (response.status == 200) {
    console.log(response.data)
    const dados = response.data as dados;
    return <Page {...dados} />
  } else {
    window.location.href = "/"
  }
}
