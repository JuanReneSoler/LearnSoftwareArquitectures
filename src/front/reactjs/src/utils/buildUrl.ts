const buildUrl = (baseUrl: string, params?: { [key: string]: any }): string => {
  const filteredParams: { [key: string]: any } = {};

  for (const entries of Object.entries(params ?? {})) {
    filteredParams[entries[0]] = entries[1];
  }

  const queryString = Object.keys(filteredParams)
    .map(
      (key) =>
        `${encodeURIComponent(key)}=${encodeURIComponent(filteredParams[key])}`
    )
    .join("&");
  return `${baseUrl}?${queryString}`;
};
export { buildUrl };
