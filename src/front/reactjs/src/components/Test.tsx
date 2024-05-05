import { useEffect } from "react";
import { TaskService } from "../services";

const abort = new AbortController();

function Test() {
  useEffect(() => {
    (async () => {
      await TaskService.List(abort).then((res) => {
        console.log(res);
      });
    })();
    abort.abort();
  }, []);

  return (
    <div>
      <p>Body</p>
    </div>
  );
}

export { Test };
