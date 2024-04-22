import React from 'react';
import styled from 'styled-components';

const HeaderContainer = styled.header`
  background-color: ${(props) => props.theme.body};
  color: ${(props) => props.theme.text};
  padding: 20px;
`;

const Header = ({ toggleTheme, themeName }) => {
  // const buttonName = themeName === 'light' ? 'Dark' : 'Default';
  return (
    <HeaderContainer>
     
      <button className='p-5 themebtn'  onClick={toggleTheme}>Theme button</button>
    </HeaderContainer>
  );
};

export default Header;



