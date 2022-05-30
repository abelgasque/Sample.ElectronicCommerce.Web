export const environment = {
  version: "1.1.2905.1",
  production: false,
  baseUrl: "https://localhost:5001",
  tokenWhitelistedDomains: [
    new RegExp('localhost:5001'), 
  ],
  tokenBlacklistedRoutes: [ 
    new RegExp('\/ws\/Token\/Login'),
  ]
};
