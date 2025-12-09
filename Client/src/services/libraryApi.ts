import axios from 'axios';
import { Author, Book, Genre } from '../types';

const api = axios.create({
  baseURL: 'http://localhost:5000/api/v1'
});

// Genres
export async function getGenres(): Promise<Genre[]> {
  const { data } = await api.get<Genre[]>('/genres');
  return data;
}

export async function createGenre(payload: Omit<Genre, 'id'>) {
  const { data } = await api.post<Genre>('/genres', payload);
  return data;
}

export async function updateGenre(id: number, payload: Omit<Genre, 'id'>) {
  const { data } = await api.put<Genre>(`/genres/${id}`, payload);
  return data;
}

export async function deleteGenre(id: number) {
  await api.delete(`/genres/${id}`);
}

// Authors
export async function getAuthors(): Promise<Author[]> {
  const { data } = await api.get<Author[]>('/authors');
  return data;
}

export async function createAuthor(payload: Omit<Author, 'id'>) {
  const { data } = await api.post<Author>('/authors', payload);
  return data;
}

export async function updateAuthor(id: number, payload: Omit<Author, 'id'>) {
  const { data } = await api.put<Author>(`/authors/${id}`, payload);
  return data;
}

export async function deleteAuthor(id: number) {
  await api.delete(`/authors/${id}`);
}

// Books
export async function getBooks(): Promise<Book[]> {
  const { data } = await api.get<Book[]>('/books');
  return data;
}

export async function createBook(payload: Omit<Book, 'id' | 'genreName' | 'authorName'>) {
  const { data } = await api.post<Book>('/books', payload);
  return data;
}

export async function updateBook(id: number, payload: Omit<Book, 'id' | 'genreName' | 'authorName'>) {
  const { data } = await api.put<Book>(`/books/${id}`, payload);
  return data;
}

export async function deleteBook(id: number) {
  await api.delete(`/books/${id}`);
}
