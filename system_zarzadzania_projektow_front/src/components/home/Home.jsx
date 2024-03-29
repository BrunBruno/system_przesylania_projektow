import { useNavigate } from "react-router-dom";
import { useCallback, useEffect, useState } from "react";
import axios from "axios";
import { baseUrl, authorization } from "../../ApiOptions.js";
import classes from "./Home.module.css";
import JSZip from "jszip";

function Home() {
  const roles = {
    student: 1,
    teacher: 2,
  };

  const navigate = useNavigate();
  const [authorize, setAuthorize] = useState(false);
  const [userInfo, setUserInfo] = useState(null);

  useEffect(() => {
    const verifyUsersToken = async () => {
      if (localStorage.getItem("token")) {
        try {
          const user = await axios.get(
            `${baseUrl}/user`,
            authorization(localStorage.getItem("token"))
          );

          localStorage.setItem("userInfo", JSON.stringify(user.data));
          setUserInfo(user.data);

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

  const GetProjects = useCallback(async () => {
    try {
      const projects = await axios.get(
        `${baseUrl}/project?name=${currentName}`,

        authorization(localStorage.getItem("token"))
      );

      setProjectList(projects.data);
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
    setSelectedProject(null);
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
      await axios.delete(
        `${baseUrl}/task/${id}`,
        authorization(localStorage.getItem("token"))
      );

      editProject(selectedProject.id);
    } catch (err) {
      console.log(err);
    }
  };

  const joinStudent = async (id) => {
    try {
      await axios.post(
        `${baseUrl}/student/join`,
        { projectId: id },
        authorization(localStorage.getItem("token"))
      );

      GetProjects();
    } catch (err) {
      console.log(err);
    }
  };

  const acceptStudent = async (studentId) => {
    try {
      await axios.put(
        `${baseUrl}/student/accept`,
        { studentId: studentId },
        authorization(localStorage.getItem("token"))
      );

      editProject(selectedProject.id);
    } catch (err) {
      console.log(err);
    }
  };

  const rejectStudent = async (studentId) => {
    try {
      await axios.delete(
        `${baseUrl}/student/${studentId}`,
        authorization(localStorage.getItem("token"))
      );

      editProject(selectedProject.id);
    } catch (err) {
      console.log(err);
    }
  };

  const checkStudent = (project) => {
    const student = project.students.filter(
      (student) => student.userId === userInfo.id
    )[0];

    if (student) {
      if (student.isAccepted) {
        return (
          <p className={classes.e} onClick={() => editProject(project.id)}>
            Przeglądaj projekt
          </p>
        );
      } else {
        return (
          <span style={{ color: "#fcc419", fontSize: "1.2rem" }}>
            Oczekiwanie na akceptacje...
          </span>
        );
      }
    } else {
      return (
        <p
          className={classes.e}
          style={{ color: "#fff" }}
          onClick={() => joinStudent(project.id)}
        >
          Dołącz do projectu
        </p>
      );
    }
  };

  const addSolution = async (event, taskId) => {
    try {
      const file = event.target.files[0];
      const formData = new FormData();

      formData.append("projectId", selectedProject.id);
      formData.append("taskId", taskId);
      formData.append("file", file);

      await axios.post(`${baseUrl}/solution`, formData, {
        headers: {
          Authorization: `Bearer ${localStorage.getItem("token")}`,
          "Content-Type": "multipart/form-data",
        },
      });

      editProject(selectedProject.id);
    } catch (err) {
      console.log(err);
    }
  };

  const searchSolutions = (taskId) => {
    const student = selectedProject.students.filter(
      (s) => s.userId === userInfo.id
    )[0];
    const solutions = student.solutions.filter((s) => s.taskId === taskId);

    return solutions;
  };

  const deleteSolution = async (solutionId) => {
    try {
      await axios.delete(
        `${baseUrl}/solution/${solutionId}`,
        authorization(localStorage.getItem("token"))
      );

      editProject(selectedProject.id);
    } catch (err) {
      console.log(err);
    }
  };

  function base64ToArrayBuffer(base64) {
    var binaryString = window.atob(base64);
    var binaryLen = binaryString.length;
    var bytes = new Uint8Array(binaryLen);
    for (var i = 0; i < binaryLen; i++) {
      var ascii = binaryString.charCodeAt(i);
      bytes[i] = ascii;
    }
    return bytes;
  }

  const downloadSolution = async (solutionId) => {
    try {
      const response = await axios.get(
        `${baseUrl}/solution/${solutionId}`,
        authorization(localStorage.getItem("token"))
      );

      var blob = new Blob([base64ToArrayBuffer(response.data.fileContent)], {
        type: response.data.contentType,
      });

      var link = document.createElement("a");
      link.href = window.URL.createObjectURL(blob);

      link.download = response.data.fileName;
      link.click();

      document.body.removeChild(link);
    } catch (err) {
      console.log(err);
    }
  };

  const sanitizeFolderName = (name) => {
    return name.replace(/[<>:"\/\\|?*]/g, "");
  };

  const downloadSolutions = async (projectId) => {
    try {
      const response = await axios.get(
        `${baseUrl}/solution/all-${projectId}`,
        authorization(localStorage.getItem("token"))
      );

      const zip = new JSZip();

      response.data.students.forEach((student) => {
        const studentFolder = zip.folder(student.studentName);

        const taskFoldersMap = new Map();

        student.solutions.forEach((file) => {
          const sanitizedTaskName = sanitizeFolderName(file.taskName);

          let taskFolder = taskFoldersMap.get(sanitizedTaskName);
          if (!taskFolder) {
            taskFolder = studentFolder.folder(sanitizedTaskName);
            taskFoldersMap.set(sanitizedTaskName, taskFolder);
          }

          const contentArray = base64ToArrayBuffer(file.fileContent);

          const blob = new Blob([contentArray], { type: file.contentType });
          console.log(file.fileName);
          taskFolder.file(file.fileName, blob);
        });
      });

      const zipBlob = await zip.generateAsync({ type: "blob" });

      const zipURL = URL.createObjectURL(zipBlob);

      const link = document.createElement("a");
      link.href = zipURL;

      let date = new Date();

      if (date.getMonth() <= 8) {
        date = date.getFullYear() - 1 + "_" + date.getFullYear();
      } else {
        date = date.getFullYear() + "_" + date.getFullYear() + 1;
      }

      link.setAttribute(
        "download",
        `${response.data.ownerName}_${response.data.projectName}_${date}.zip`
      );

      document.body.appendChild(link);
      link.click();

      URL.revokeObjectURL(zipURL);
      document.body.removeChild(link);
    } catch (err) {
      console.log(err);
    }
  };

  const checkOverdueSolutions = (student) => {
    if (!student.isAccepted) {
      return "";
    }

    const solutions = student.solutions;

    const overdueTasks = selectedProject.tasks.filter(
      (task) =>
        !solutions.some((solution) => solution.taskId === task.id) &&
        new Date(task.endDate) < new Date()
    );

    console.log(overdueTasks);
    console.log(solutions);

    if (overdueTasks.length === 0) {
      return "";
    }

    return (
      <div className={classes.overdue}>
        <p>Nieprzekazano:</p>
        {overdueTasks.map((otask) => (
          <p>- {otask.name}</p>
        ))}
      </div>
    );
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

        <nav>
          <p>{userInfo.email}</p>
          <p onClick={onLogOut}>LogOut</p>
        </nav>
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
                <ul className={classes.tasks}>
                  <li>ZADANIA</li>
                  {selectedProject.tasks.map((task, ti) => (
                    <li key={task.id}>
                      <p>Zadanie #{task.taskNo} </p>
                      <p>{task.name}</p>
                      <p>{task.description}</p>
                      {userInfo.role === roles.teacher && (
                        <p
                          onClick={() => deleteTask(task.id)}
                          className={classes.bin}
                        >
                          __
                          <br />
                          ||
                        </p>
                      )}
                      <p className={classes.date}>
                        {new Date(task.endDate).toLocaleDateString()}
                      </p>
                    </li>
                  ))}
                </ul>
                {userInfo.role === roles.teacher ? (
                  <ul className={classes.students}>
                    <li>STUDENCI</li>
                    {selectedProject.students.map((student, si) => (
                      <li key={student.id}>
                        <p>{student.name}</p>

                        {checkOverdueSolutions(student)}

                        {student.isAccepted && (
                          <p style={{ fontWeight: 600 }}>Przesłane pliki:</p>
                        )}
                        {student.isAccepted && (
                          <div className={classes["stud-tasks"]}>
                            {student.solutions.map((solution, soli) => (
                              <p
                                key={solution.id}
                                onClick={() => downloadSolution(solution.id)}
                              >
                                .{solution.fileName.split(".").pop()}
                                <span>{solution.fileName}</span>
                              </p>
                            ))}
                          </div>
                        )}

                        {!student.isAccepted && (
                          <div className={classes.accept}>
                            <p onClick={() => acceptStudent(student.id)}>
                              Zaakceptuj
                            </p>
                            <p onClick={() => rejectStudent(student.id)}>
                              Odrzuć
                            </p>
                          </div>
                        )}
                      </li>
                    ))}
                  </ul>
                ) : (
                  <ul className={classes.answers}>
                    <li>Dodaj rozwiązanie</li>
                    {selectedProject.tasks.map((task, ti) => (
                      <li key={task.id}>
                        <p>Zadanie #{task.taskNo}</p>
                        <p>{task.name}</p>
                        <p className={classes["p-solutions"]}>
                          {searchSolutions(task.id).map((solution, si) => (
                            <span key={solution.id}>
                              <span>{solution.fileName}</span>
                              <button
                                onClick={() => deleteSolution(solution.id)}
                              >
                                x
                              </button>
                            </span>
                          ))}
                        </p>
                        <label>
                          Dodaj plik
                          <input
                            type="file"
                            name="solution"
                            onChange={(e) => {
                              addSolution(e, task.id);
                            }}
                          />
                        </label>
                      </li>
                    ))}
                  </ul>
                )}
              </div>
            </div>
          )}
          <ul>
            {projectList.length > 0 ? (
              projectList.map((project, i) => (
                <li key={project.id}>
                  <h3>{project.name}</h3>
                  <div>
                    <p>Prowadzący: </p>
                    <p>{project.ownerName}</p>
                  </div>
                  <div>
                    <p>Ilczba uczestników: {project.studentCount}</p>
                    <p>Ilczba zadań: {project.taskCount}</p>
                  </div>
                  {userInfo.role === roles.teacher ? (
                    <div className={classes.buttons}>
                      <p onClick={() => editProject(project.id)}>Edytuj</p>
                      <p onClick={() => downloadSolutions(project.id)}>
                        Exportuj
                      </p>
                      <p onClick={() => setProjectToDelete(project.id)}>Usuń</p>
                    </div>
                  ) : (
                    <div className={classes.buttons}>
                      {checkStudent(project)}
                    </div>
                  )}
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
            {userInfo.role === roles.teacher && (
              <li>
                <form onSubmit={createProject}>
                  <h4>Stwórz repozytorium</h4>
                  <input placeholder="Nazwa Repozytorium" name="name" />
                  <button className={classes.pb} type="submit">
                    Utwórz
                  </button>
                </form>
              </li>
            )}
            <li>
              <form>
                <h4>Szukaj repozytorium</h4>
                <input
                  placeholder="Nazwa Repozytorium"
                  onChange={(e) => searchProject(e)}
                />
              </form>
            </li>
            {selectedProject && userInfo.role === roles.teacher && (
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
