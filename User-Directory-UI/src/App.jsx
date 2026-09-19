import { BrowserRouter, Routes, Route, Link } from "react-router-dom";
import UserList from "./Pages/UserList";
import AddUser from "./Pages/AddUser";
import "./App.css";

function App() {
  return (
    <BrowserRouter>
      <div className="app-container">

        <header className="navbar">
          <h1>User Directory</h1>

          <nav className="nav-links">
            <Link to="/">Users</Link>
            <Link to="/add">Add User</Link>
          </nav>
        </header>

        <main className="content">
          <Routes>
            <Route path="/" element={<UserList />} />
            <Route path="/add" element={<AddUser />} />
          </Routes>
        </main>

      </div>
    </BrowserRouter>
  );
}

export default App;