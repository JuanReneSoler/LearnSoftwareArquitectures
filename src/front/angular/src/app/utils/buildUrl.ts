const buildUrl = (baseUrl: string, params?: { [key: string]: any }): string => {
  const filterParams: { [key: string]: any } = {};

  for (const key of Object.entries(params ?? {})) {
    console.log(key);
  }

  return `${baseUrl}?`;
};
export { buildUrl };
