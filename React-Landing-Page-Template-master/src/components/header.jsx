


import React, { useState, useEffect } from "react";

export const Header = (props) => {
  
  const [homeBannerUrl, setHomeBannerUrl] = useState('');
  const [faviconUrl, setFaviconUrl] = useState('');

  useEffect(() => {
      // Fetch the favicon and home banner when the component mounts
      fetchFavicon();
      fetchHomeBanner();
    }, []);

    const fetchFavicon = async () => {
      // Determine the favicon URL based on the port
      const faviconApi = window.location.port === '3001' 
        ? 'https://localhost:44350/api/Multitenant/2/favicon'  // URL for port 3001
        : 'https://localhost:44350/api/Multitenant/1/favicon'; // Default URL for port 3000
  
      try {
        const response = await fetch(faviconApi);
        if (!response.ok) {
          throw new Error('Failed to fetch favicon');
        }
        const faviconBlob = await response.blob();
        const faviconUrl = URL.createObjectURL(faviconBlob);
        setFaviconUrl(faviconUrl);
      } catch (error) {
        console.error('Error fetching favicon:', error);
      }
    };
  
    const fetchHomeBanner = async () => {
      const homeBannerApi = window.location.port === '3001'
        ? 'https://localhost:44350/api/Multitenant/2/homebanner'  // URL for port 3001
        : 'https://localhost:44350/api/Multitenant/1/homebanner'; // Default URL for port 3000
  
      try {
        const response = await fetch(homeBannerApi);
        if (!response.ok) {
          throw new Error('Failed to fetch home banner');
        }
        const homeBannerBlob = await response.blob();
        const homeBannerUrl = URL.createObjectURL(homeBannerBlob);
        setHomeBannerUrl(homeBannerUrl);
      } catch (error) {
        console.error('Error fetching home banner:', error);
      }
    };
  
    useEffect(() => {
      // Update the favicon when the faviconUrl state changes
      if (faviconUrl) {
        updateFavicon(faviconUrl);
      }
    }, [faviconUrl]);
  const updateFavicon = (url) => {
    // Set the fetched favicon as the favicon of the webpage
    const link = document.querySelector("link[rel*='icon']") || document.createElement('link');
    link.type = 'image/x-icon';
    link.rel = 'shortcut icon';
    link.href = url;
    document.getElementsByTagName('head')[0].appendChild(link);
  };  
  

  return (
    <header id="header">
      <div className="intro">
     
        <div className="overlay">
       
          {/* <div className="container"> */}
        
            <div className="row">
              <div className="col-md-12 intro-text">
              {homeBannerUrl && <img src={homeBannerUrl} alt="Logo"   style={{
        width: '100', // Adjust width as needed
        height: 'auto', // Maintain aspect ratio
         // Apply border radius to create oval shape
        overflow: 'hidden', // Ensure image doesn't overflow the oval shape
      }} />}
                {/* <h1>
                  {props.data ? props.data.title : "Loading"}
                  <span></span>
                </h1>
                <p>{props.data ? props.data.paragraph : "Loading"}</p> */}
                <a
                  href="#features"
                  className="btn btn-custom mt-4 btn-lg page-scroll"
                >
                  Learn More
                </a>{" "}
              </div>
            </div>
          </div>
        </div>
      {/* </div> */}
    </header>
  );
};



