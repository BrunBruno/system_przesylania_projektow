import { useRef } from "react";
import { useNavigate } from "react-router-dom";
import axios from "axios";

import { baseUrl } from "../../ApiOptions.js";

import classes from "./Index.module.css";

function RegisterModal() {
  const emailErrRef = useRef(null);
  const firstNameErrRef = useRef(null);
  const lastNameErrRef = useRef(null);
  const passwordErrRef = useRef(null);

  const navigate = useNavigate();

  const registerUser = async (event) => {
    event.preventDefault();

    const userData = {
      firstName: event.target.firstName.value.trim(),
      lastName: event.target.lastName.value.trim(),
      email: event.target.email.value.trim(),
      password: event.target.password.value,
      confirmPassword: event.target.confirmPassword.value,
    };

    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailRegex.test(userData.email)) {
      emailErrRef.current.classList.add(classes.error);
      emailErrRef.current.innerHTML = "Nieprawidłoy email.";
      return;
    } else {
      emailErrRef.current.classList.remove(classes.error);
      emailErrRef.current.innerHTML = "";
    }

    if (userData.password.length < 5) {
      passwordErrRef.current.classList.add(classes.error);
      passwordErrRef.current.innerHTML = "Haso musi mieć conajmniej 5 znaków.";
      return;
    } else {
      passwordErrRef.current.classList.remove(classes.error);
      passwordErrRef.current.innerHTML = "";
    }

    if (userData.password !== userData.confirmPassword) {
      passwordErrRef.current.classList.add(classes.error);
      passwordErrRef.current.innerHTML = "Hasła nie pasują do siebie.";
      return;
    } else {
      passwordErrRef.current.classList.remove(classes.error);
      passwordErrRef.current.innerHTML = "";
    }

    try {
      await axios.post(`${baseUrl}/user/register`, userData);

      const logUserData = {
        email: event.target.email.value.trim(),
        password: event.target.password.value,
      };

      localStorage.setItem("userTemp", JSON.stringify(logUserData));

      const response = await axios.post(`${baseUrl}/user/sign-in`, logUserData);

      localStorage.setItem("token", response.data.token);

      navigate("/home");
    } catch (err) {
      if (err.response && err.response.data) {
        emailErrRef.current.classList.add(classes.error);
        emailErrRef.current.innerHTML = err.response.data;
      } else {
        console.log("Connection error.");
      }
    }
  };

  return (
    <form className={classes.form} onSubmit={registerUser}>
      <h2>Zarejestruj się.</h2>
      <div className={classes["form-container"]}>
        <div className={classes.row}>
          <span>Imię</span>
          <input type="text" name="firstName" autoComplete="off" />
          <span ref={firstNameErrRef}></span>
        </div>
        <div className={classes.row}>
          <span>Nazwisko</span>
          <input type="text" name="lastName" autoComplete="off" />
          <span ref={lastNameErrRef}></span>
        </div>
        <div className={classes.row}>
          <span>Email</span>
          <input type="text" name="email" autoComplete="off" />
          <span ref={emailErrRef}></span>
        </div>
        <div className={classes.row}>
          <span>Hasło</span>
          <input type="password" name="password" autoComplete="off" />
          <span ref={passwordErrRef}></span>
        </div>
        <div className={classes.row}>
          <span>Potwierdź hasło</span>
          <input type="password" name="confirmPassword" autoComplete="off" />
        </div>
        <div className={classes.buttons}>
          <button type="submit">Register</button>
        </div>
      </div>
    </form>
  );
}

export default RegisterModal;
