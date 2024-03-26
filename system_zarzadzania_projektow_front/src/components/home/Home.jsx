import { useNavigate } from "react-router-dom";
import { useEffect, useState } from "react";
import axios from "axios";
import { baseUrl, authorization } from "../../ApiOptions.js";
import classes from "./Home.module.css";

function Home() {
  const navigate = useNavigate();

  const [authorize, setAuthorize] = useState(false);

  useEffect(() => {
    const verifyUsersToken = async () => {
      if (localStorage.getItem("token")) {
        try {
          const user = await axios.get(
            `${baseUrl}/user`,
            authorization(localStorage.getItem("token"))
          );

          localStorage.setItem("userInfo", JSON.stringify(user.data));

          setAuthorize(true);
        } catch (err) {
          navigate("/");
        }
      } else {
        navigate("/");
      }
    };

    verifyUsersToken();
  }, []);

  if (!authorize) {
    // return <LoadingPage />;
    return "";
  }

  return <div className={classes.container}></div>;
}

export default Home;
