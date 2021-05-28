import { createAuthService } from '@tourmalinecore/react-tc-auth';

export const authService = createAuthService({
  authApiRoot: 'http://localhost:5000/auth',
  authType: 'ls',
  tokenAccessor: 'accessToken',
  refreshTokenAccessor: 'refreshToken',
  tokenValueAccessor: 'value',
  tokenExpireAccessor: 'expiresInUtc',
});