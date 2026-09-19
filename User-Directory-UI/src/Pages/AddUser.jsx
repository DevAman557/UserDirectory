import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { createUser } from "../services/userService";

function AddUser() {
  const navigate = useNavigate();

  const [form, setForm] = useState({
    name: "",
    age: "",
    city: "",
    state: "",
    pincode: ""
  });

  const [errors, setErrors] = useState({});
  const [serverError, setServerError] = useState("");
  const [saving, setSaving] = useState(false);

  function handleChange(e) {
    const { name, value } = e.target;

    setForm((previous) => ({
      ...previous,
      [name]: value
    }));
  }

  function validate() {
    const newErrors = {};

    const name = form.name.trim();
    const city = form.city.trim();
    const state = form.state.trim();
    const pincode = form.pincode.trim();

    if (!name) {
      newErrors.name = "Name is required.";
    } else if (name.length < 2 || name.length > 100) {
      newErrors.name = "Name must be between 2 and 100 characters.";
    }

    if (form.age === "") {
      newErrors.age = "Age is required.";
    } else if (!Number.isInteger(Number(form.age))) {
      newErrors.age = "Age must be a whole number.";
    } else if (Number(form.age) < 0 || Number(form.age) > 120) {
      newErrors.age = "Age must be between 0 and 120.";
    }

    if (!city) {
      newErrors.city = "City is required.";
    }

    if (!state) {
      newErrors.state = "State is required.";
    }

    if (!pincode) {
      newErrors.pincode = "Pincode is required.";
    } else if (pincode.length < 4 || pincode.length > 10) {
      newErrors.pincode =
        "Pincode must be between 4 and 10 characters.";
    }

    return newErrors;
  }

  async function handleSubmit(e) {
    e.preventDefault();

    setServerError("");

    const validationErrors = validate();

    if (Object.keys(validationErrors).length > 0) {
      setErrors(validationErrors);
      return;
    }

    try {
      setErrors({});
      setSaving(true);

      await createUser({
        name: form.name.trim(),
        age: Number(form.age),
        city: form.city.trim(),
        state: form.state.trim(),
        pincode: form.pincode.trim()
      });

      navigate("/", {
        state: {
          successMessage: "User created successfully."
        }
      });
    } catch (error) {
      setServerError(error.message);
    } finally {
      setSaving(false);
    }
  }

  return (
    <div className="form-page">

      <div className="form-container">

        <div className="form-header">
          <h2>Add User</h2>
          <p>Enter the user's information below.</p>
        </div>

        {serverError && (
          <div className="error-message">
            {serverError}
          </div>
        )}

        <form onSubmit={handleSubmit}>

          <div className="form-group">
            <label htmlFor="name">
              Name <span className="required">*</span>
            </label>

            <input
              id="name"
              name="name"
              type="text"
              placeholder="Enter full name"
              value={form.name}
              onChange={handleChange}
            />

            {errors.name && (
              <p className="validation-error">
                {errors.name}
              </p>
            )}
          </div>

          <div className="form-group">
            <label htmlFor="age">
              Age <span className="required">*</span>
            </label>

            <input
              id="age"
              name="age"
              type="number"
              min="0"
              max="120"
              placeholder="Enter age"
              value={form.age}
              onChange={handleChange}
            />

            {errors.age && (
              <p className="validation-error">
                {errors.age}
              </p>
            )}
          </div>

          <div className="form-group">
            <label htmlFor="city">
              City <span className="required">*</span>
            </label>

            <input
              id="city"
              name="city"
              type="text"
              placeholder="Enter city"
              value={form.city}
              onChange={handleChange}
            />

            {errors.city && (
              <p className="validation-error">
                {errors.city}
              </p>
            )}
          </div>

          <div className="form-group">
            <label htmlFor="state">
              State <span className="required">*</span>
            </label>

            <input
              id="state"
              name="state"
              type="text"
              placeholder="Enter state"
              value={form.state}
              onChange={handleChange}
            />

            {errors.state && (
              <p className="validation-error">
                {errors.state}
              </p>
            )}
          </div>

          <div className="form-group">
            <label htmlFor="pincode">
              Pincode <span className="required">*</span>
            </label>

            <input
              id="pincode"
              name="pincode"
              type="text"
              placeholder="Enter pincode"
              value={form.pincode}
              onChange={handleChange}
            />

            {errors.pincode && (
              <p className="validation-error">
                {errors.pincode}
              </p>
            )}
          </div>

          <button
            type="submit"
            className="primary-button"
            disabled={saving}
          >
            {saving ? "Saving..." : "Add User"}
          </button>

        </form>
      </div>

    </div>
  );
}

export default AddUser;