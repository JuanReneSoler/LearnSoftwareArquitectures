import { useEffect } from "react";
import { TaskService } from "../services";

function Test() {
  useEffect(() => {
    (async () => {
      await TaskService.List().then((res) => {
        console.log(res);
      });
    })();
  }, []);

  return (
    <div>
      <p>Body</p>
    </div>
  );
}

export { Test };
