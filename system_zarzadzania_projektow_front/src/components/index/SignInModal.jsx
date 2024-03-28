import { useRef } from "react";
import { useNavigate } from "react-router-dom";
import axios from "axios";

import { baseUrl } from "../../ApiOptions.js";

import classes from "./Index.module.css";

function SignInModal() {
  const emailErrRef = useRef(null);
  const passwordErrRef = useRef(null);

  const navigate = useNavigate();

  const loginUser = async (event) => {
    event.preventDefault();

    const userData = {
      email: event.target.email.value.trim(),
      password: event.target.password.value,
    };

    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailRegex.test(userData.email)) {
      emailErrRef.current.classList.add(classes.error);
      emailErrRef.current.innerHTML = "Nieporawny email.";
      return;
    } else {
      emailErrRef.current.classList.remove(classes.error);
      emailErrRef.current.innerHTML = "";
    }

    if (userData.password.length === 0) {
      passwordErrRef.current.classList.add(classes.error);
      passwordErrRef.current.innerHTML = "Hasło nie może być puste.";
      return;
    } else {
      passwordErrRef.current.classList.remove(classes.error);
      passwordErrRef.current.innerHTML = "";
    }

    try {
      const response = await axios.post(`${baseUrl}/user/sign-in`, userData);

      localStorage.setItem("token", response.data.token);

      navigate("/home");
    } catch (err) {
      console.log(err.response.data);
      if (err.response && err.response.data) {
        emailErrRef.current.classList.add(classes.error);
        emailErrRef.current.innerHTML = err.response.data;
      } else {
        console.log("Connection error.");
      }
    }
  };

  return (
    <form className={classes.form} onSubmit={loginUser}>
      <h2>Zaloguj się.</h2>
      <div className={classes["form-container"]}>
        <div className={classes.row}>
          <span>Email</span>
          <input type="text" name="email" autoComplete="off"></input>
          <span ref={emailErrRef}></span>
        </div>

        <div className={classes.row}>
          <span>Hasło</span>
          <input type="password" name="password" autoComplete="off"></input>
          <span ref={passwordErrRef}></span>
        </div>

        <div className={classes.buttons}>
          <button type="submit">Log In.</button>
        </div>
      </div>
    </form>
  );
}

export default SignInModal;
