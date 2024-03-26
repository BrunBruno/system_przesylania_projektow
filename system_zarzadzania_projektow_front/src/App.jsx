import { BrowserRouter, Routes, Route } from "react-router-dom";

import Index from "./components/index/Index";
import Home from "./components/home/Home";
import "./App.css";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/*" element={<Index />} />
        <Route path="/home/*" element={<Home />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
