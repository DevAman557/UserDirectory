import { render, screen, waitFor } from "@testing-library/react";
import { MemoryRouter } from "react-router-dom";
import { vi } from "vitest";
import UserList from "../Pages/UserList";
import { getUsers } from "../services/userService";

vi.mock("../services/userService", () => ({
  getUsers: vi.fn()
}));

describe("UserList", () => {
  beforeEach(() => {
    vi.clearAllMocks();
  });

  test("should display users when API returns users", async () => {
    getUsers.mockResolvedValue([
      {
        id: 1,
        name: "Aman Kumar",
        age: 30,
        city: "Delhi",
        state: "Delhi",
        pincode: "110001"
      },
      {
        id: 2,
        name: "Rajni",
        age: 28,
        city: "Mumbai",
        state: "Maharashtra",
        pincode: "400001"
      }
    ]);

    render(
      <MemoryRouter>
        <UserList />
      </MemoryRouter>
    );

    expect(screen.getByText("Loading users...")).toBeInTheDocument();

    await waitFor(() => {
      expect(screen.getByText("Aman Kumar")).toBeInTheDocument();
    });

    expect(screen.getByText("Rajni")).toBeInTheDocument();
    expect(screen.getAllByText("Delhi")).toHaveLength(2);
    expect(screen.getByText("110001")).toBeInTheDocument();
  });

  test("should display no users message when API returns empty list", async () => {
    getUsers.mockResolvedValue([]);

    render(
      <MemoryRouter>
        <UserList />
      </MemoryRouter>
    );

    await waitFor(() => {
      expect(screen.getByText("No users found.")).toBeInTheDocument();
    });
  });

  test("should display error message when API fails", async () => {
    getUsers.mockRejectedValue(
      new Error("Failed to load users.")
    );

    render(
      <MemoryRouter>
        <UserList />
      </MemoryRouter>
    );

    await waitFor(() => {
      expect(
        screen.getByText("Failed to load users.")
      ).toBeInTheDocument();
    });

    expect(screen.getByText("Retry")).toBeInTheDocument();
  });
});