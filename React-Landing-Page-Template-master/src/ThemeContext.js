// // ThemeContext.js
// import React, { createContext, useContext, useState } from "react";

// const ThemeContext = createContext();

// export const useTheme = () => useContext(ThemeContext);

// export const ThemeProvider = ({ children }) => {
//   const [theme, setTheme] = useState("light");

//   const toggleTheme = () => {
//     setTheme((prevTheme) => (prevTheme === "light" ? "dark" : "light"));
//   };

//   return (
//     <ThemeContext.Provider value={{ theme, toggleTheme }}>
//       {children}
//     </ThemeContext.Provider>
//   );
// };




// Global.js
import { createGlobalStyle } from 'styled-components';

export const GlobalStyles = createGlobalStyle`
  body {
    background-color: ${(props) => props.theme.body};
    color: ${(props) => props.theme.text};
    font-family: Arial, sans-serif;
    margin: 0;
    padding: 0;
  }
`;
