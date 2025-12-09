import { Link, Navigate, Route, Routes, useLocation } from 'react-router-dom';
import HomePage from './pages/HomePage';
import GenresPage from './pages/GenresPage';
import AuthorsPage from './pages/AuthorsPage';
import BooksPage from './pages/BooksPage';

function NavBar() {
  const { pathname } = useLocation();
  const linkClass = (href: string) =>
    pathname === href ? 'nav-link active' : 'nav-link';

  return (
    <header className="nav">
      <div className="logo">Teste Fullstack</div>
      <nav>
        <Link className={linkClass('/')} to="/">
          Início
        </Link>
        <Link className={linkClass('/genres')} to="/genres">
          Gêneros
        </Link>
        <Link className={linkClass('/authors')} to="/authors">
          Autores
        </Link>
        <Link className={linkClass('/books')} to="/books">
          Livros
        </Link>
      </nav>
    </header>
  );
}

export default function App() {
  return (
    <div className="app-shell">
      <NavBar />
      <main className="page">
        <Routes>
          <Route path="/" element={<HomePage />} />
          <Route path="/genres" element={<GenresPage />} />
          <Route path="/authors" element={<AuthorsPage />} />
          <Route path="/books" element={<BooksPage />} />
          <Route path="*" element={<Navigate to="/" />} />
        </Routes>
      </main>
    </div>
  );
}
