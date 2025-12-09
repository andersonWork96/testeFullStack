import { FormEvent, useEffect, useState } from 'react';
import { createGenre, deleteGenre, getGenres, updateGenre } from '../services/libraryApi';
import { ApiMessage, Genre } from '../types';

const emptyForm = { name: '', description: '' };

export default function GenresPage() {
  const [genres, setGenres] = useState<Genre[]>([]);
  const [form, setForm] = useState(emptyForm);
  const [editingId, setEditingId] = useState<number | null>(null);
  const [message, setMessage] = useState<ApiMessage | null>(null);

  useEffect(() => {
    load();
  }, []);

  async function load() {
    const data = await getGenres();
    setGenres(data);
  }

  function handleChange(e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) {
    setForm({ ...form, [e.target.name]: e.target.value });
  }

  async function handleSubmit(e: FormEvent) {
    e.preventDefault();
    try {
      if (editingId) {
        const updated = await updateGenre(editingId, form);
        setGenres((prev) => prev.map((g) => (g.id === editingId ? updated : g)));
        setMessage({ type: 'success', text: 'Gênero atualizado.' });
      } else {
        const created = await createGenre(form);
        setGenres((prev) => [...prev, created]);
        setMessage({ type: 'success', text: 'Gênero criado.' });
      }
      setForm(emptyForm);
      setEditingId(null);
    } catch (error) {
      setMessage({ type: 'error', text: 'Erro ao salvar gênero.' });
    }
  }

  async function handleDelete(id: number) {
    await deleteGenre(id);
    setGenres((prev) => prev.filter((g) => g.id !== id));
  }

  function startEdit(genre: Genre) {
    setEditingId(genre.id);
    setForm({ name: genre.name, description: genre.description ?? '' });
  }

  return (
    <section className="card">
      <div className="section-header">
        <h2>Gêneros</h2>
        {message && <span className="badge">{message.text}</span>}
      </div>
      <form className="form" onSubmit={handleSubmit}>
        <input
          name="name"
          placeholder="Nome"
          value={form.name}
          onChange={handleChange}
          required
        />
        <textarea
          name="description"
          placeholder="Descrição"
          value={form.description}
          onChange={handleChange}
        />
        <div className="actions">
          <button type="submit">{editingId ? 'Atualizar' : 'Criar'}</button>
          {editingId && (
            <button type="button" onClick={() => { setEditingId(null); setForm(emptyForm); }}>
              Cancelar
            </button>
          )}
        </div>
      </form>

      <div className="card-grid">
        {genres.map((genre) => (
          <div className="card" key={genre.id}>
            <h3>{genre.name}</h3>
            <small>{genre.description || 'Sem descrição'}</small>
            <div className="actions" style={{ marginTop: 12 }}>
              <button onClick={() => startEdit(genre)}>Editar</button>
              <button className="danger" onClick={() => handleDelete(genre.id)}>
                Excluir
              </button>
            </div>
          </div>
        ))}
      </div>
    </section>
  );
}
