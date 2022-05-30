export const environment = {
  version: "1.1.3005.2",
  production: false,
  baseUrl: "https://localhost:5001",
  tokenWhitelistedDomains: [
    new RegExp('localhost:5001'), 
  ],
  tokenBlacklistedRoutes: [ 
    new RegExp('\/ws\/Token\/Login'),
  ]
};
