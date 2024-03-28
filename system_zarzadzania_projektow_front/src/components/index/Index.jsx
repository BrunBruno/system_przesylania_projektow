import { useState } from "react";
import classes from "./Index.module.css";
import RegisterModal from "./RegisterModal.jsx";
import SignInModal from "./SignInModal.jsx";

function Index() {
  const [displayedModal, setDisplayedModal] = useState(<SignInModal />);

  return (
    <div className={classes.container}>
      <header className={classes.header}>
        <p
          onClick={() => {
            window.location.reload();
          }}
        >
          SYSTEM ZARZĄDZANIA PROJEKTAMI
        </p>
        <nav className={classes.nav}>
          <ul>
            <li
              onClick={() => {
                setDisplayedModal(<RegisterModal />);
              }}
            >
              Register
            </li>
            <li
              onClick={() => {
                setDisplayedModal(<SignInModal />);
              }}
            >
              LogIn
            </li>
          </ul>
        </nav>
      </header>
      <div className={classes.modal}>{displayedModal}</div>
    </div>
  );
}

export default Index;
