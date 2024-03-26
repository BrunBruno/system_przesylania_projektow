export const baseUrl = " http://localhost:5131/api";

export const authorization = (token) => {
  return {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  };
};
