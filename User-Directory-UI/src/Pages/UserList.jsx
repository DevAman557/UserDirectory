import { useEffect, useState } from "react";
import { useLocation } from "react-router-dom";
import { getUsers } from "../services/userService";

function UserList() {
  const location = useLocation();

  const [users, setUsers] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");

  useEffect(() => {
    loadUsers();
  }, []);

  async function loadUsers() {
    try {
      setLoading(true);
      setError("");

      const data = await getUsers();

      setUsers(data);
    } catch (error) {
      setError(error.message);
    } finally {
      setLoading(false);
    }
  }

  if (loading) {
    return (
      <div className="loading">
        <h2>User List</h2>
        <p>Loading users...</p>
      </div>
    );
  }

  return (
    <div>
      <h2 className="page-title">User List</h2>

      {location.state?.successMessage && (
        <div className="success-message">
          {location.state.successMessage}
        </div>
      )}

      {error && (
        <div className="error-message">
          {error}
          <br />
          <button onClick={loadUsers}>Retry</button>
        </div>
      )}

      {!error && users.length === 0 && (
        <div className="empty-state">
          <p>No users found.</p>
        </div>
      )}

      {!error && users.length > 0 && (
        <div className="table-container">
          <table className="user-table">
            <thead>
              <tr>
                <th>Name</th>
                <th>Age</th>
                <th>City</th>
                <th>State</th>
                <th>Pincode</th>
              </tr>
            </thead>

            <tbody>
              {users.map((user) => (
                <tr key={user.id}>
                  <td>{user.name}</td>
                  <td>{user.age}</td>
                  <td>{user.city}</td>
                  <td>{user.state}</td>
                  <td>{user.pincode}</td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}
    </div>
  );
}

export default UserList;