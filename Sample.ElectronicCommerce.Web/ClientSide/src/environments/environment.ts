export const environment = {
  version: "1.1.0206.3",
  production: false,
  baseUrl: "https://localhost:5001",
  tokenWhitelistedDomains: [
    new RegExp('localhost:5001'), 
  ],
  tokenBlacklistedRoutes: [ 
    new RegExp('\/ws\/Token\/Login'),
  ]
};
