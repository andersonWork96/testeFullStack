import { FormEvent, useEffect, useState } from 'react';
import {
  createBook,
  deleteBook,
  getAuthors,
  getBooks,
  getGenres,
  updateBook
} from '../services/libraryApi';
import { ApiMessage, Author, Book, Genre } from '../types';

const emptyForm = {
  title: '',
  synopsis: '',
  publishDate: '',
  genreId: '',
  authorId: ''
};

export default function BooksPage() {
  const [books, setBooks] = useState<Book[]>([]);
  const [authors, setAuthors] = useState<Author[]>([]);
  const [genres, setGenres] = useState<Genre[]>([]);
  const [form, setForm] = useState<typeof emptyForm>(emptyForm);
  const [editingId, setEditingId] = useState<number | null>(null);
  const [message, setMessage] = useState<ApiMessage | null>(null);

  useEffect(() => {
    load();
  }, []);

  async function load() {
    const [booksData, authorsData, genresData] = await Promise.all([
      getBooks(),
      getAuthors(),
      getGenres()
    ]);
    setBooks(booksData);
    setAuthors(authorsData);
    setGenres(genresData);
  }

  function handleChange(e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>) {
    setForm({ ...form, [e.target.name]: e.target.value });
  }

  async function handleSubmit(e: FormEvent) {
    e.preventDefault();

    const payload = {
      title: form.title,
      synopsis: form.synopsis || undefined,
      publishDate: form.publishDate || undefined,
      genreId: Number(form.genreId),
      authorId: Number(form.authorId)
    };

    try {
      if (editingId) {
        const updated = await updateBook(editingId, payload);
        setBooks((prev) => prev.map((b) => (b.id === editingId ? updated : b)));
        setMessage({ type: 'success', text: 'Livro atualizado.' });
      } else {
        const created = await createBook(payload);
        setBooks((prev) => [...prev, created]);
        setMessage({ type: 'success', text: 'Livro criado.' });
      }
      setForm(emptyForm);
      setEditingId(null);
    } catch (error) {
      setMessage({ type: 'error', text: 'Erro ao salvar livro.' });
    }
  }

  async function handleDelete(id: number) {
    await deleteBook(id);
    setBooks((prev) => prev.filter((b) => b.id !== id));
  }

  function startEdit(book: Book) {
    setEditingId(book.id);
    setForm({
      title: book.title,
      synopsis: book.synopsis || '',
      publishDate: book.publishDate ? book.publishDate.substring(0, 10) : '',
      genreId: String(book.genreId),
      authorId: String(book.authorId)
    });
  }

  return (
    <section className="card">
      <div className="section-header">
        <h2>Livros</h2>
        {message && <span className="badge">{message.text}</span>}
      </div>
      <form className="form" onSubmit={handleSubmit}>
        <input
          name="title"
          placeholder="Título"
          value={form.title}
          onChange={handleChange}
          required
        />
        <textarea
          name="synopsis"
          placeholder="Sinopse"
          value={form.synopsis}
          onChange={handleChange}
        />
        <input
          type="date"
          name="publishDate"
          value={form.publishDate}
          onChange={handleChange}
        />
        <select name="genreId" value={form.genreId} onChange={handleChange} required>
          <option value="" disabled>
            Selecione um gênero
          </option>
          {genres.map((g) => (
            <option key={g.id} value={g.id}>
              {g.name}
            </option>
          ))}
        </select>
        <select name="authorId" value={form.authorId} onChange={handleChange} required>
          <option value="" disabled>
            Selecione um autor
          </option>
          {authors.map((a) => (
            <option key={a.id} value={a.id}>
              {a.name}
            </option>
          ))}
        </select>
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
        {books.map((book) => (
          <div className="card" key={book.id}>
            <h3>{book.title}</h3>
            <small>{book.genreName}</small>
            <p>{book.synopsis || 'Sem sinopse'}</p>
            <div className="badge" style={{ marginBottom: 8 }}>
              {book.authorName}
            </div>
            <div className="actions">
              <button onClick={() => startEdit(book)}>Editar</button>
              <button className="danger" onClick={() => handleDelete(book.id)}>
                Excluir
              </button>
            </div>
          </div>
        ))}
      </div>
    </section>
  );
}
