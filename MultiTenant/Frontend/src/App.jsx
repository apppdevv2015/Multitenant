import React, { useState, useEffect } from 'react';
import { ThemeProvider } from 'styled-components';
import { lightTheme, darkTheme } from './Theme';
import { GlobalStyles } from './ThemeContext';
import { Navigation } from './components/navigation';
import { Header } from './components/header';
import { Features } from './components/features';
import { About } from './components/about';
import { Services } from './components/services';
import { Gallery } from './components/gallery';
import { Testimonials } from './components/testimonials';
import { Team } from './components/Team';
import { Contact } from './components/contact';
import SmoothScroll from "smooth-scroll";
import JsonData from "./data/data.json";
import Headers from './Headers';

// Initialize SmoothScroll
const scroll = new SmoothScroll('a[href*="#"]', {
  speed: 1000,
  speedAsDuration: true,
});

function App() {
  const [landingPageData, setLandingPageData] = useState({});
  const [themeData, setThemeData] = useState(null);
  const [theme, setTheme] = useState('light');
  const [initialThemeSet, setInitialThemeSet] = useState(false);

  useEffect(() => {
    async function fetchThemeData() {
      try {
        // Get the hostname from the window location
        const host = window.location.host;
        // Fetch theme data based on hostname
        const response = await fetch(`https://localhost:44350/api/Multitenant/GetAllTenantsAsync?host=${host}`);
        if (!response.ok) {
          throw new Error('Failed to fetch theme data');
        }
        const data = await response.json();
        setThemeData(data);
        setTheme(data.themeName === 'dark' ? 'dark' : 'light');
        setInitialThemeSet(true); // Set flag indicating initial theme has been set
    
        // Update URL for foo and bar hosts
        if (host === 'localhost:3000') {
          // Update URL to 'foo'
          window.location.href = 'http://127.0.0.1:3000/Foo';
        } else if (host === 'localhost:3001') {
          // Update URL to 'bar'
          window.location.href = 'http://127.0.0.1:3001/Bar';
        }
      } catch (error) {
        console.error('Error fetching theme data:', error);
      }
    };
    fetchThemeData();
    setLandingPageData(JsonData);

    function handleKeyDown(e) {
      if (e.key.toLowerCase() === 'r') {  // Use 'r' key to toggle ports
        const currentPort = window.location.port;
        const newPort = currentPort === '3000' ? '3001' : '3000';
        const newLocation = window.location.href.replace(`:${currentPort}`, `:${newPort}`);
        window.location.href = newLocation;
      }
    }

    window.addEventListener('keydown', handleKeyDown);
    return () => window.removeEventListener('keydown', handleKeyDown);
  }, []);

  const toggleTheme = () => {
    setTheme(prevTheme => prevTheme === 'light' ? 'dark' : 'light');
  };

  // Use initialThemeSet flag to determine the initial theme
  const initialTheme = initialThemeSet ? theme : 'light';

  return (
    <ThemeProvider theme={initialTheme === 'light' ? lightTheme : darkTheme}>
      <GlobalStyles />
      <Navigation />
      <Header data={landingPageData.Header} />
      <Features data={landingPageData.Features} />
      <About data={landingPageData.About} />
      <Services data={landingPageData.Services} />
      <Gallery data={landingPageData.Gallery} />
      <Testimonials data={landingPageData.Testimonials} />
      {themeData && <Headers toggleTheme={toggleTheme} themeName={themeData.themeName} />}
      <Team data={landingPageData.Team} />
      <Contact data={landingPageData.Contact} />
    </ThemeProvider>
  );
}

export default App;