import React from 'react';
import ReactDOM from 'react-dom';
import App from './App';
import { authService } from './services/authService';

ReactDOM.render(
  <React.StrictMode>
     <authService.AuthProvider>
      <App />
    </authService.AuthProvider>
  </React.StrictMode>,
  document.getElementById('root')
);
