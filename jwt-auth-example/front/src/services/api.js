import axios from 'axios';

import { authService } from './authService';

export const api = axios.create({
  baseURL: 'http://localhost:5000',
});

api.interceptors.request.use((config) => {
  const token = authService.getAuthToken();

  config.headers.Authorization = token ? `Bearer ${token}` : '';

  return config;
}, null);

api.interceptors.response.use(null, async (error) => {
  if (
    (error.response && error.response.status === 401)
    || (error.response && error.response.status === 403)
  ) {
    if (!authService.getAuthToken()){
      return Promise.reject(error);
    }

    await authService.refreshToken();
    const token = authService.getAuthToken();
    error.config.headers.Authorization = token ? `Bearer ${token}` : '';
    return api.request(error.config);
  }

  return Promise.reject(error);
});
