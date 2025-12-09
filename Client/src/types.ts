export type Genre = {
  id: number;
  name: string;
  description?: string;
};

export type Author = {
  id: number;
  name: string;
  bio?: string;
  birthDate?: string;
};

export type Book = {
  id: number;
  title: string;
  synopsis?: string;
  publishDate?: string;
  genreId: number;
  authorId: number;
  genreName?: string;
  authorName?: string;
};

export type ApiMessage = {
  type: 'success' | 'error';
  text: string;
};
