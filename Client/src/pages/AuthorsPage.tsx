import { FormEvent, useEffect, useState } from 'react';
import { createAuthor, deleteAuthor, getAuthors, updateAuthor } from '../services/libraryApi';
import { ApiMessage, Author } from '../types';

const emptyForm = { name: '', bio: '', birthDate: '' };

export default function AuthorsPage() {
  const [authors, setAuthors] = useState<Author[]>([]);
  const [form, setForm] = useState(emptyForm);
  const [editingId, setEditingId] = useState<number | null>(null);
  const [message, setMessage] = useState<ApiMessage | null>(null);

  useEffect(() => {
    load();
  }, []);

  async function load() {
    const data = await getAuthors();
    setAuthors(data);
  }

  function handleChange(e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) {
    setForm({ ...form, [e.target.name]: e.target.value });
  }

  async function handleSubmit(e: FormEvent) {
    e.preventDefault();
    const payload = { ...form, birthDate: form.birthDate || undefined };

    try {
      if (editingId) {
        const updated = await updateAuthor(editingId, payload);
        setAuthors((prev) => prev.map((a) => (a.id === editingId ? updated : a)));
        setMessage({ type: 'success', text: 'Autor atualizado.' });
      } else {
        const created = await createAuthor(payload);
        setAuthors((prev) => [...prev, created]);
        setMessage({ type: 'success', text: 'Autor criado.' });
      }
      setForm(emptyForm);
      setEditingId(null);
    } catch (error) {
      setMessage({ type: 'error', text: 'Erro ao salvar autor.' });
    }
  }

  async function handleDelete(id: number) {
    await deleteAuthor(id);
    setAuthors((prev) => prev.filter((a) => a.id !== id));
  }

  function startEdit(author: Author) {
    setEditingId(author.id);
    setForm({
      name: author.name,
      bio: author.bio || '',
      birthDate: author.birthDate ? author.birthDate.substring(0, 10) : ''
    });
  }

  return (
    <section className="card">
      <div className="section-header">
        <h2>Autores</h2>
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
          name="bio"
          placeholder="Bio"
          value={form.bio}
          onChange={handleChange}
        />
        <input
          type="date"
          name="birthDate"
          value={form.birthDate}
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
        {authors.map((author) => (
          <div className="card" key={author.id}>
            <h3>{author.name}</h3>
            <small>{author.bio || 'Sem bio'}</small>
            <div className="actions" style={{ marginTop: 12 }}>
              <button onClick={() => startEdit(author)}>Editar</button>
              <button className="danger" onClick={() => handleDelete(author.id)}>
                Excluir
              </button>
            </div>
          </div>
        ))}
      </div>
    </section>
  );
}
