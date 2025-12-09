export default function HomePage() {
  return (
    <section className="card">
      <h2>Teste Full Stack</h2>
      <p>
        CRUD completo para gêneros, autores e livros. Utilize o menu para navegar entre as telas e
        gerenciar os registros. O frontend está em React + Vite e conversa com a API .NET em
        <code>/api/v1</code>.
      </p>
      <p>
        Obs: o backend está usando SQLite local (<code>testeFullstack.db</code>) para facilitar a execução
        sem instalação de MySQL/SQL Server/PostgreSQL neste ambiente. Para usar outro banco basta
        trocar a connection string e o provider no <code>Program.cs</code> e aplicar migrations.
      </p>
    </section>
  );
}
