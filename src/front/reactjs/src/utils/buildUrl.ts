const buildUrl = (baseUrl: string, params?: { [key: string]: any }): string => {
  const filteredParams: { [key: string]: any } = {};

  for (const entry of Object.entries(params ?? {})) {
    if (entry[0]) filteredParams[entry[0]] = entry[1];
  }

  const queryString = Object.keys(filteredParams)
    .map(
      (key) =>
        `${encodeURIComponent(key)}=${encodeURIComponent(filteredParams[key])}`
    )
    .join("&");
  return `${baseUrl}${queryString !== "" ? "?" + queryString : ""}`;
};
export { buildUrl };
