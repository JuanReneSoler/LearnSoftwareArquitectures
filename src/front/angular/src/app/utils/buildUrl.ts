const buildUrl = (baseUrl: string, params?: { [key: string]: any }): string => {
  const filterParams: { [key: string]: any } = {};

  for (const entri of Object.entries(params ?? {})) {
    if (entri[0]) filterParams[entri[0]] = entri[1];
  }

  const queryString = Object.keys(filterParams)
    .map(
      (key) =>
        `${encodeURIComponent(key)}=${encodeURIComponent(filterParams[key])}`
    )
    .join('&');

  return `${baseUrl}${queryString !== '' ? '?' + queryString : ''}`;
};
export { buildUrl };
