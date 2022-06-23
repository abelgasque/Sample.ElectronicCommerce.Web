export const environment = {
  version: "1.1.0206.3",
  production: false,
  baseUrl: "https://localhost:5001",
  tokenWhitelistedDomains: [
    "localhost:5001",
  ],
  tokenBlacklistedRoutes: [
    "https://localhost:5001/security/auth",
  ]
};
