import { useNavigate } from "react-router-dom";
import { useCallback, useEffect, useState } from "react";
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

  const onLogOut = () => {
    localStorage.removeItem("userInfo");
    localStorage.removeItem("token");

    navigate("/");
  };

  const [selectedProjectName, setSelectedProjectName] = useState("");
  const [selectedProject, setSelectedProject] = useState(null);
  const [projectToDelete, setProjectToDelete] = useState(null);

  useEffect(() => {
    if (selectedProject) {
      setSelectedProjectName(selectedProject.name);
    } else {
      setSelectedProjectName(null);
    }
  }, [selectedProject]);

  const [currentName, setCurrentName] = useState("");

  const [projectList, setProjectList] = useState([]);
  const [totalPages, setTotalPages] = useState(0);
  const [totalProjects, setTotalProjects] = useState(0);

  const GetProjects = useCallback(async () => {
    try {
      const projects = await axios.get(
        `${baseUrl}/project?name=${currentName}&pageNumber=1&ElementsCount=8`,

        authorization(localStorage.getItem("token"))
      );

      setProjectList(projects.data.items);
      setTotalPages(projects.data.totalPages);
      setTotalProjects(projects.data.totalItemsCount);
    } catch (err) {
      console.log(err);
    }
  }, [currentName, selectedProject]);

  useEffect(() => {
    GetProjects();
  }, [GetProjects]);

  const createProject = async (event) => {
    event.preventDefault();

    const project = {
      projectName: event.target.name.value.trim(),
    };

    if (project.name === "") {
      return;
    }

    try {
      await axios.post(
        `${baseUrl}/project`,
        project,
        authorization(localStorage.getItem("token"))
      );

      location.reload();
    } catch (err) {
      console.log(err);
    }
  };

  const searchProject = (event) => {
    setCurrentName(event.target.value.toLowerCase());
  };

  const deleteProject = async (id) => {
    try {
      await axios.delete(
        `${baseUrl}/project/${id}`,
        authorization(localStorage.getItem("token"))
      );

      location.reload();
    } catch (err) {
      console.log(err);
    }
  };

  const editProject = async (id) => {
    try {
      const project = await axios.get(
        `${baseUrl}/project/${id}`,
        authorization(localStorage.getItem("token"))
      );

      setSelectedProject(project.data);
    } catch (err) {
      console.log(err);
    }
  };

  const changeProjectName = async (event) => {
    try {
      const projectToUodate = {
        projectId: selectedProject.id,
        name: event.target.value,
      };

      console.log(projectToUodate);

      await axios.put(
        `${baseUrl}/project/${selectedProject.id}`,
        projectToUodate,
        authorization(localStorage.getItem("token"))
      );
    } catch (err) {
      console.log(err);
    }
  };

  const addTask = async (event) => {
    event.preventDefault();

    const task = {
      projectId: selectedProject.id,
      name: event.target.name.value.trim(),
      description: event.target.description.value.trim(),
      endDate: event.target.endDate.value.trim(),
    };

    try {
      await axios.post(
        `${baseUrl}/task`,
        task,
        authorization(localStorage.getItem("token"))
      );

      editProject(selectedProject.id);
    } catch (err) {
      console.log(err);
    }
  };
  const deleteTask = async (id) => {
    try {
      console.log(id);
      await axios.delete(
        `${baseUrl}/task/${id}`,
        authorization(localStorage.getItem("token"))
      );

      editProject(selectedProject.id);
    } catch (err) {
      console.log(err);
    }
  };

  if (!authorize) {
    return <div className={classes.loading} />;
  }

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

        <p onClick={onLogOut}>LogOut</p>
      </header>

      <div className={classes.content}>
        <div className={classes.repositories}>
          {selectedProject && (
            <div className={classes.editor}>
              <h1>
                <input
                  type="text"
                  value={selectedProjectName ? selectedProjectName : ""}
                  onChange={(e) => {
                    setSelectedProjectName(e.target.value);
                  }}
                  onBlur={(e) => {
                    changeProjectName(e);
                  }}
                />
                <p
                  className={classes.x}
                  onClick={() => {
                    setSelectedProject(null);
                  }}
                >
                  X
                </p>
              </h1>
              <div className={classes.split}>
                <ul>
                  <li>ZADANIA</li>
                  {selectedProject.tasks.map((task, ti) => (
                    <li key={ti}>
                      <p>Zadanie #{ti + 1} </p>
                      <p>{task.name}</p>
                      <p>{task.description}</p>
                      <p onClick={() => deleteTask(task.id)}>
                        __
                        <br />
                        ||
                      </p>
                      <p>{new Date(task.endDate).toLocaleDateString()}</p>
                    </li>
                  ))}
                </ul>
                <ul>
                  <li>STUDENCI</li>
                  {selectedProject.students.map((student, si) => (
                    <li key={si}></li>
                  ))}
                </ul>
              </div>
            </div>
          )}
          <ul>
            {projectList.length > 0 ? (
              projectList.map((project, i) => (
                <li key={i}>
                  <h3>{project.name}</h3>
                  <div>
                    <p>Prowadzący: </p>
                    <p>{project.ownerName}</p>
                  </div>
                  <div>
                    <p>Ilczba uczestników: {project.studentCount}</p>
                    <p>Ilczba zadań: {project.taskCount}</p>
                  </div>
                  <div>
                    <p
                      className={classes.e}
                      onClick={() => editProject(project.id)}
                    >
                      Edytuj repozytorum
                    </p>
                    <p
                      className={classes.d}
                      onClick={() => setProjectToDelete(project.id)}
                    >
                      Usuń repozytorium
                    </p>
                  </div>
                  <div></div>
                </li>
              ))
            ) : (
              <div>Brak wyników.</div>
            )}
          </ul>
        </div>
        <div className={classes.controls}>
          <ul>
            <li>
              <form onSubmit={createProject}>
                <h4>Stwórz repozytorium</h4>
                <input placeholder="Nazwa Repozytorium" name="name" />
                <button className={classes.pb} type="submit">
                  Utwórz
                </button>
              </form>
            </li>
            <li>
              <form>
                <h4>Szukaj repozytorium</h4>
                <input
                  placeholder="Nazwa Repozytorium"
                  onChange={(e) => searchProject(e)}
                />
              </form>
            </li>
            {selectedProject && (
              <li>
                <form onSubmit={addTask}>
                  <h4>Dodaj zadanie</h4>
                  <input placeholder="Nazwa Zadania" name="name" />
                  <textarea placeholder="Treść" name="description"></textarea>
                  <input type="date" name="endDate" />
                  <button className={classes.pb} type="submit">
                    Dodaj
                  </button>
                </form>
              </li>
            )}
            {projectToDelete && (
              <li>
                <p>Czy na pewno chcesz usunąć repozytorium?</p>
                <div>
                  <button onClick={() => deleteProject(projectToDelete)}>
                    TAK
                  </button>
                  <button onClick={() => setProjectToDelete(null)}>NIE</button>
                </div>
              </li>
            )}
          </ul>
        </div>
      </div>
    </div>
  );
}

export default Home;
