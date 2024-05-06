import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App.tsx";
import "./index.css";
import { SesionProvider } from "./containers/sesion.tsx";

ReactDOM.createRoot(document.getElementById("root")!).render(
  <React.StrictMode>
    <SesionProvider>
      <App />
    </SesionProvider>
  </React.StrictMode>
);
